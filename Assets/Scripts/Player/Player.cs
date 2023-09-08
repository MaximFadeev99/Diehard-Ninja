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
    #endregion

    #region PossibleSuperStates
    public GroundedSuperState GroundedSuperState { get; private set; }
    public AirbornSuperState AirbornSuperState { get; private set; }
    public WallTouchingSuperState WallTouchingSuperState { get; private set; }
    #endregion

    #region Utilities
    private Timer _timer;
    #endregion

    #region CheckBooleans
    private bool _isGrounded;
    public bool IsGrounded    
    { 
        get => _isGrounded;
        private set 
        {
            _isGrounded = value;
            Debug.Log("isGrounded: " +  _isGrounded);
        }    
    }

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

    public bool IsTouchingWallRight;
    public bool IsTouchingWallLeft;
    public bool IsWallJumping;

    #endregion

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerData = GetComponent<PlayerData>();
        _timer = new Timer();
        StateMachine = new StateMachine(this);
        GroundedSuperState = new GroundedSuperState(StateMachine);
        AirbornSuperState = new AirbornSuperState(StateMachine);
        WallTouchingSuperState = new WallTouchingSuperState(StateMachine);
        StateMachine.Reset();
        DoSurfaceRaycasts();
    }

    private void OnEnable()
    {
        InputHandler.JumpButtonPushed += OnJumpButtonPushed;
        InputHandler.JumpButtonReleased += ResetJumping;
        //_timer.TimeIsUp += OnTimerExpired;
    }

    private void OnDisable()
    {
        InputHandler.JumpButtonPushed -= OnJumpButtonPushed;
        InputHandler.JumpButtonReleased += ResetJumping;
        //_timer.TimeIsUp -= OnTimerExpired;
    }


    private void Update()
    {
        if (_timer.IsActive)
            _timer.Tick();

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
            _timer.TimeIsUp += ResetJumping;
            _timer.StartTimer(PlayerData.JumpDuration);
        }
        else if (IsGrounded) 
        {
            IsJumping = true;
            _timer.TimeIsUp += ResetJumping;
            _timer.StartTimer(PlayerData.JumpDuration);
        }             
    }

    public void ResetJumping() 
    {
        IsJumping = false;
        IsWallJumping = false;
        _timer.TimeIsUp -= ResetJumping;
    }

    private void DoSurfaceRaycasts() 
    {
        IsGrounded = Physics2D.Raycast
            (transform.position, Vector2.down, PlayerData.GroundDistanceCheck, PlayerData.WallLayerMask);
        IsTouchingWallLeft = Physics2D.Raycast
            (transform.position, Vector2.left, PlayerData.WallDistanceCheck, PlayerData.WallLayerMask);
        IsTouchingWallRight = Physics2D.Raycast
            (transform.position, Vector2.right, PlayerData.WallDistanceCheck, PlayerData.WallLayerMask);
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
}
