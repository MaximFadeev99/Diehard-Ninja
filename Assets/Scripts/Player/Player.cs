using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour, IDamagable
{
    #region Fields, Properties, Actions

    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerData _playerData;

    public PlayerData PlayerData => _playerData;
    public int AvailableGoldIngots { get; private set; } = 0;

    #region Components 
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler {  get; private set; }
    public PlayerStateMachine PlayerStateMachine { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public AudioSource AudioSource { get; private set; }
    public DashDirectionIndicator DashDirectionIndicator { get; private set; }
    public ParticleSystem DashParticleSystem { get; private set; }
    #endregion

    #region Utilities 
    private List<Timer> _timers = new();
    private Timer _jumpResetTimer;
    private Timer _coyoteJumpTimer;
    private Timer _attackTimer;
    private Timer _deflectionCoolDownTimer;
    private Timer _dashTimer;

    public Timer AttackTimer => _attackTimer;
    #endregion

    #region State Control Booleans
    public bool IsAwakening { get; private set; } = true;
    public bool IsDead { get; private set; } = false;  
    public bool IsGrounded { get; private set; } = true;
    public bool IsGroundedInPreviousFrame { get; private set; } = true;
    public bool IsJumping { get; private set; } = false;
    public bool IsCoyoteTimeActive { get; private set; } = true;
    public bool IsTouchingWallRight { get; private set; } = false;
    public bool IsTouchingWallLeft { get; private set; } = false;
    public bool IsWallJumping { get; private set; } = false;
    public bool IsBlocking { get; private set; } = false;
    public bool IsAttacking { get; private set; } = false;
    public bool IsUsingAbility { get; private set; } = false;
    public bool IsDeflecting { get; private set; } = false;
    public bool IsDashing { get; private set; } = false;
    #endregion

    #region Others
    public bool IsFacingRight = true;
    public bool IsUsingAttackDash = false;
    private bool _canDeflect = true;
    private bool _canDash = true;   
    private bool _areRaycastsPaused = false;
    private bool _isInvincible = false;
    private string DashDirectionIndicatorName = nameof(DashDirectionIndicator);
    private string DashAfterImage = nameof(DashAfterImage);

    public bool IsDashOnHold { get; private set; } = false;
    #endregion

    #region  Actions
    public Action HealthChanged;
    public Action DashStarted;
    public Action DashEnded;
    public Action DeflectionStarted;
    public Action DeflectionEnded;
    public Action<int> GoldIngotNumberChanged;
    #endregion

    #endregion

    #region Methods

    #region MonoBehaviour
    private void Awake()
    {
        GetRequiredComponents();
        CreateTimers();
        PlayerStateMachine = new (this);
        PlayerStateMachine.Reset();
        DoSurfaceRaycasts();
    }

    private void OnEnable() => DoInitialSubscriptions(); 

    private void OnDisable() => UndoInitialSubscriptions();

    private void Update()
    {
        foreach (Timer timer in _timers) 
        {
            if (timer.IsActive)
                timer.Tick();
        }
        
        if (_areRaycastsPaused == false)
            DoSurfaceRaycasts();

        IsBlocking = InputHandler.IsBlocking;
        PlayerStateMachine.DoLogicUpdate();
    }

    private void FixedUpdate() => PlayerStateMachine.DoPhysicsUpdate();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,
            transform.position + Vector3.down * PlayerData.GroundDistanceCheck);
        Gizmos.DrawLine(transform.position,
            transform.position + Vector3.right * PlayerData.WallDistanceCheck);
        Gizmos.DrawLine(transform.position,
            transform.position + Vector3.left * PlayerData.WallDistanceCheck);
    }
    #endregion

    #region Called OnAwake
    private void GetRequiredComponents()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
        DashDirectionIndicator = transform.Find(DashDirectionIndicatorName).GetComponent<DashDirectionIndicator>();
        DashParticleSystem = transform.Find(DashAfterImage).GetComponent<ParticleSystem>();
    }

    private void CreateTimers()
    {
        _jumpResetTimer = new ();
        _coyoteJumpTimer = new ();
        _attackTimer = new ();
        _deflectionCoolDownTimer = new ();
        _dashTimer = new ();
        _timers.Add(_jumpResetTimer);
        _timers.Add(_coyoteJumpTimer);
        _timers.Add(_attackTimer);
        _timers.Add(_deflectionCoolDownTimer);
        _timers.Add(_dashTimer);
    }

    private void DoInitialSubscriptions()
    {
        _playerHealth.PlayerDied += ActivateDeadState;
        InputHandler.JumpButtonPushed += OnJumpButtonPushed;
        InputHandler.JumpButtonReleased += ResetJumping;
        InputHandler.MainAttackButtonPushed += ActivateAttack;
        InputHandler.DeflectionButtonPushed += StartDeflectionAbility;
        InputHandler.DashButtonPushed += StartDashAbility;
        InputHandler.DashButtonReleased += ResetDashOnHold;
        _coyoteJumpTimer.TimeIsUp += DeactivateCoyoteTime;
        _attackTimer.TimeIsUp += ResetAttackTimer;
        _deflectionCoolDownTimer.TimeIsUp += ResetDeflectionAbility;
    }

    private void UndoInitialSubscriptions()
    {
        _playerHealth.PlayerDied -= ActivateDeadState;
        InputHandler.JumpButtonPushed -= OnJumpButtonPushed;
        InputHandler.JumpButtonReleased -= ResetJumping;
        InputHandler.MainAttackButtonPushed -= ActivateAttack;
        InputHandler.DeflectionButtonPushed -= StartDeflectionAbility;
        InputHandler.DashButtonPushed -= StartDashAbility;
        InputHandler.DashButtonReleased -= ResetDashOnHold;
        _coyoteJumpTimer.TimeIsUp -= DeactivateCoyoteTime;
        _attackTimer.TimeIsUp -= ResetAttackTimer;
        _deflectionCoolDownTimer.TimeIsUp -= ResetDeflectionAbility;
    }
    #endregion

    #region Jump Related
    public void ResetJumping()
    {
        IsJumping = false;
        IsWallJumping = false;
        _jumpResetTimer.TimeIsUp -= ResetJumping;
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

    private void TryActivateCoyoteTime() 
    {
        if (IsGroundedInPreviousFrame && IsGrounded == false)
        {
            IsCoyoteTimeActive = true;
            _coyoteJumpTimer.Start(PlayerData.CoyoteJumpTime);
        }

        IsGroundedInPreviousFrame = IsGrounded;
    }

    private void DeactivateCoyoteTime() => IsCoyoteTimeActive = false;
    #endregion

    #region Raycast Related
    public void AnnulAndPauseWallRaycats()
    {
        _areRaycastsPaused = true;
        IsTouchingWallLeft = false;
        IsTouchingWallRight = false;
    }

    public void ResumeRaycasts() => _areRaycastsPaused = false;

    private void DoSurfaceRaycasts()
    {
        IsGrounded = Physics2D.Raycast
            (transform.position, Vector2.down, PlayerData.GroundDistanceCheck, PlayerData.WallLayerMask);
        IsTouchingWallLeft = Physics2D.Raycast
            (transform.position, Vector2.left, PlayerData.WallDistanceCheck, PlayerData.WallLayerMask);
        IsTouchingWallRight = Physics2D.Raycast
            (transform.position, Vector2.right, PlayerData.WallDistanceCheck, PlayerData.WallLayerMask);
        TryActivateCoyoteTime();       
    }
    #endregion

    #region Attack & Damage Related
    public void ActivateAttack()
    {
        if (IsGrounded)
            IsAttacking = true;
    }
 
    public void TakeDamage(float incomingDamage)
    {
        if (_isInvincible == false)
            HealthChanged?.Invoke();
    }

    public void DeactivateAttack() => IsAttacking = false; //used by Animation Event
    public void DashForward() => IsUsingAttackDash = true; //used by Animation Event
    public void ActivateInvincibilty() => _isInvincible = true;
    public void DeactivateInvincibility() => _isInvincible = false;
    private void ResetAttackTimer() => PlayerStateMachine.GroundedSuperState.AttackState.ResetAttackTimer();
    private void ActivateDeadState() => IsDead = true;
    #endregion

    #region Deflection Ability Related
    public void StartDeflectionAbility()
    {
        if (_canDeflect && IsUsingAbility == false)
        {
            IsUsingAbility = true;
            IsDeflecting = true;
            _canDeflect = false;
            DeflectionStarted?.Invoke();
        }
    }

    public void EndDeflectionAbility()
    {
        IsDeflecting = false;
        IsUsingAbility = false;
        _deflectionCoolDownTimer.Start(PlayerData.DeflectionCoolDownTime);
        DeflectionEnded?.Invoke();
    }

    private void ResetDeflectionAbility() => _canDeflect = true;
    #endregion

    #region Dash Ability Related
    public void StartDashAbility()
    {
        if (_canDash && IsUsingAbility == false)
        {
            IsUsingAbility = true;
            IsDashing = true;
            _canDash = false;
            IsDashOnHold = true;
            DashStarted?.Invoke();
            _dashTimer.TimeIsUp += ResetDashOnHold;
            _dashTimer.Start(PlayerData.DashHoldTime);
        }
    }
   
    public void EndDashAbility()
    {
        IsDashing = false;
        IsUsingAbility = false;
        _dashTimer.TimeIsUp -= EndDashAbility;
        PlayerStateMachine.AbilitySuperState.DashState.ForceExit();
        _dashTimer.Start(PlayerData.DashCoolDownTime);
        _dashTimer.TimeIsUp += ResetDashAbility;
        DashEnded?.Invoke();
    }
    private void ResetDashOnHold()
    {
        if (IsDashing)
        {
            IsDashOnHold = false;
            _dashTimer.TimeIsUp -= ResetDashOnHold;
            _dashTimer.TimeIsUp += EndDashAbility;
            _dashTimer.Start(PlayerData.DashDuration);
        }
    }

    private void ResetDashAbility()
    {
        _canDash = true;
        _dashTimer.TimeIsUp -= ResetDashAbility;
    }
    #endregion

    #region Others   
    public void AddGoldIngots(int addedAmount)
    {
        AvailableGoldIngots += addedAmount;
        GoldIngotNumberChanged?.Invoke(AvailableGoldIngots);
    }

    public void ExitAwakeState() => IsAwakening = false;  //used by Animation Event
    #endregion

    #endregion
}
