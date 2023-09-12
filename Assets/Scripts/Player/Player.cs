using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components 
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler {  get; private set; }
    public StateMachine StateMachine { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerData PlayerData { get; private set; }
    public DashDirectionIndicator DashDirectionIndicator { get; private set; }
    public ParticleSystem DashParticleSystem { get; private set; }
    #endregion

    #region PossibleSuperStates
    public GroundedSuperState GroundedSuperState { get; private set; }
    public AirbornSuperState AirbornSuperState { get; private set; }
    public WallTouchingSuperState WallTouchingSuperState { get; private set; }
    public AbilitySuperState AbilitySuperState { get; private set; }
    public DeadSuperState DeadSuperState { get; private set; }
    #endregion

    #region Utilities 
    private List<Timer> _timers = new List<Timer>();
    private Timer _jumpResetTimer;
    private Timer _coyoteJumpTimer;
    private Timer _attackTimer;
    private Timer _deflectionCoolDownTimer;
    private Timer _dashTimer;
    #endregion

    #region StateControlBooleans
    [SerializeField] public bool IsDead = false;
    
    private bool _isGrounded;
    public bool IsGrounded    
    { 
        get => _isGrounded;
        private set 
        {
            _isGrounded = value;
            //Debug.Log("isGrounded: " +  _isGrounded);
        }    
    }

    public bool IsGroundedInPreviousFrame = true;

    private bool _isJumping;

    public bool IsJumping 
    { 
        get => _isJumping;
        private set 
        {
            _isJumping = value;
            //Debug.Log(_isJumping);
        } 
    }

    public bool IsCoyoteTimeActive = false;

    public bool IsTouchingWallRight { get; private set; }
    public bool IsTouchingWallLeft { get; private set; }
    public bool IsWallJumping { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsUsingAbility { get; private set; }
    public bool IsDeflecting { get; private set; }
    public bool IsDashing { get; private set; }
    public bool IsDashOnHold { get; private set; }
    private bool _canDeflect = true;
    private bool _canDash = true;

    public bool IsFacingRight = true;
    public bool IsUsingAttackDash = false;
    private bool _areRaycastsPaused = false;
    private bool _isInvincible = false;
    private float _attackResetTime = 1f;

    #endregion

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerData = GetComponent<PlayerData>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator").GetComponent<DashDirectionIndicator>();
        DashParticleSystem = transform.Find("DashAfterImage").GetComponent<ParticleSystem>();
        _jumpResetTimer = new Timer();
        _coyoteJumpTimer = new Timer();
        _attackTimer = new Timer();
        _deflectionCoolDownTimer = new Timer();
        _dashTimer = new Timer();
        _timers.Add(_jumpResetTimer);
        _timers.Add(_coyoteJumpTimer);
        _timers.Add(_attackTimer);
        _timers.Add(_deflectionCoolDownTimer);
        _timers.Add(_dashTimer);
        StateMachine = new StateMachine(this);
        GroundedSuperState = new GroundedSuperState(StateMachine);
        AirbornSuperState = new AirbornSuperState(StateMachine);
        WallTouchingSuperState = new WallTouchingSuperState(StateMachine);
        AbilitySuperState = new AbilitySuperState(StateMachine);
        DeadSuperState = new DeadSuperState(StateMachine);
        StateMachine.Reset();
        DoSurfaceRaycasts();
    }

    private void OnEnable()
    {
        InputHandler.JumpButtonPushed += OnJumpButtonPushed;
        InputHandler.JumpButtonReleased += ResetJumping;
        InputHandler.MainAttackButtonPushed += ActivateAttack;
        InputHandler.DeflectionButtonPushed += StartDeflectionAbility;
        InputHandler.DashButtonPushed += StartDashAbility;
        InputHandler.DashButtonReleased += ResetDashOnHold;
        _coyoteJumpTimer.TimeIsUp += DeactivateCoyoteTime;
        _attackTimer.TimeIsUp += ResetAttackTimer;
        _deflectionCoolDownTimer.TimeIsUp += ResetDeflectionAbility;
        //_timer.TimeIsUp += OnTimerExpired;
    }

    private void OnDisable()
    {
        InputHandler.JumpButtonPushed -= OnJumpButtonPushed;
        InputHandler.JumpButtonReleased -= ResetJumping;
        InputHandler.MainAttackButtonPushed -= ActivateAttack;
        InputHandler.DeflectionButtonPushed -= StartDeflectionAbility;
        InputHandler.DashButtonPushed -= StartDashAbility;
        InputHandler.DashButtonReleased -= ResetDashOnHold;
        _coyoteJumpTimer.TimeIsUp -= DeactivateCoyoteTime;
        _attackTimer.TimeIsUp -= ResetAttackTimer;
        _deflectionCoolDownTimer.TimeIsUp -= ResetDeflectionAbility;
        //_timer.TimeIsUp -= OnTimerExpired;
    }


    private void Update()
    {
        foreach (Timer timer in _timers) 
        {
            if (timer.IsActive)
                timer.Tick();
        }        
        
        UpdateBattleInputs();

        if (_areRaycastsPaused == false)
            DoSurfaceRaycasts();

        StateMachine.DoLogicUpdate();

    }

    private void FixedUpdate()
    {
        StateMachine.DoPhysicsUpdate();
        //Debug.Log(StateMachine.CurrentSuperState.CurrentState + " + " + Rigidbody.velocity);
    }

    public void ExitAwakeState() 
    {
        StateMachine.ExitAwakeState();
    }

    private void OnJumpButtonPushed() 
    {
        if ((IsTouchingWallLeft || IsTouchingWallRight))
        {
            IsWallJumping = true;           
        }
        else if (IsGrounded || IsCoyoteTimeActive) 
        {
            IsJumping = true;
        }

        _jumpResetTimer.TimeIsUp += ResetJumping;
        _jumpResetTimer.Start(PlayerData.JumpDuration);
    }

    public void ResetJumping() 
    {
        IsJumping = false;
        IsWallJumping = false;
        _jumpResetTimer.TimeIsUp -= ResetJumping;
    }

    private void DoSurfaceRaycasts() 
    {
        IsGrounded = Physics2D.Raycast
            (transform.position, Vector2.down, PlayerData.GroundDistanceCheck, PlayerData.WallLayerMask);
        IsTouchingWallLeft = Physics2D.Raycast
            (transform.position, Vector2.left, PlayerData.WallDistanceCheck, PlayerData.WallLayerMask);
        IsTouchingWallRight = Physics2D.Raycast
            (transform.position, Vector2.right, PlayerData.WallDistanceCheck, PlayerData.WallLayerMask);

        if (IsGroundedInPreviousFrame && IsGrounded == false) 
        {
            IsCoyoteTimeActive = true;
            _coyoteJumpTimer.Start(PlayerData.CoyoteJumpTime);
        }          

        IsGroundedInPreviousFrame = IsGrounded;
        
        //Debug.Log("Touching wall left:" + IsTouchingWallLeft);
        //Debug.Log("Touching wall right:" + IsTouchingWallRight);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine (transform.position, 
            transform.position + Vector3.down * PlayerData.GroundDistanceCheck);
        Gizmos.DrawLine(transform.position,
            transform.position + Vector3.right * PlayerData.WallDistanceCheck);
        Gizmos.DrawLine(transform.position,
            transform.position + Vector3.left * PlayerData.WallDistanceCheck);
    }

    private void OnTimerExpired()  // test method for timer
    {
        Debug.Log("Time is up");
    }

    private void UpdateBattleInputs() 
    {
        IsBlocking = InputHandler.IsBlocking;
    }

    public void AnnulAndPauseWallRaycats() 
    {
        _areRaycastsPaused = true;
        IsTouchingWallLeft = false;
        IsTouchingWallRight = false;
    }

    public void DashForward()
    {
        IsUsingAttackDash = true;
    }

    public void ResumeRaycasts() => _areRaycastsPaused = false;
    public void ActivateInvincibilty() => _isInvincible = true;
    public void DeactivateInvincibility() => _isInvincible = false;
    private void DeactivateCoyoteTime() => IsCoyoteTimeActive = false;
    public void ActivateAttack()
    {
        if (IsGrounded) 
            IsAttacking = true;               
    } 
    
    public void DeactivateAttack() 
    {
        IsAttacking = false;
        //InputHandler.ResetAttackInputBlock();
        _attackTimer.Start(_attackResetTime);
    }

    public void StartDeflectionAbility()
    {
        if (_canDeflect && IsUsingAbility == false) 
        {
            IsUsingAbility = true;
            IsDeflecting = true;
            _canDeflect = false;           
        }           
    }

    public void EndDeflectionAbility() 
    {
        IsDeflecting = false;
        IsUsingAbility = false;
        _deflectionCoolDownTimer.Start(PlayerData.DeflectionCoolDownTime);
    }

    private void ResetDeflectionAbility() => _canDeflect = true;

    public void StartDashAbility() 
    {
        if (_canDash && IsUsingAbility == false)
        {
            IsUsingAbility = true;
            IsDashing = true;
            _canDash = false;
            IsDashOnHold = true;
            _dashTimer.TimeIsUp += ResetDashOnHold;
            _dashTimer.Start(PlayerData.DashHoldTime);
        }
    }
    private void ResetDashOnHold() 
    {
        IsDashOnHold = false;
        _dashTimer.TimeIsUp -= ResetDashOnHold;
        _dashTimer.TimeIsUp += EndDashAbility;
        _dashTimer.Start(PlayerData.DashDuration);
    } 

    public void EndDashAbility()
    {
        IsDashing = false;
        IsUsingAbility = false;
        _dashTimer.TimeIsUp -= EndDashAbility;
        _dashTimer.TimeIsUp += ResetDashAbility;
        _dashTimer.Start(PlayerData.DashCoolDownTime);
    }

    private void ResetDashAbility() 
    {
        _canDash = true;
        _dashTimer.TimeIsUp -= ResetDashAbility;
    }


    private void ResetAttackTimer() => GroundedSuperState.AttackState.ResetTimer();
}
