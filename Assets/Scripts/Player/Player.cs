using System.Collections;
using System.Collections.Generic;
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
    #endregion

    #region Utilities
    private Timer _timer;
    #endregion

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
    public float FacindDirection = 1;

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
        StateMachine.Reset();
        CheckIfGrounded();
    }

    private void OnEnable()
    {
        InputHandler.JumpButtonPushed += OnJumpButtonPushed;
        InputHandler.JumpButtonReleased += ResetJumping;
        _timer.TimeIsUp += OnTimerExpired;
    }

    private void OnDisable()
    {
        InputHandler.JumpButtonPushed -= OnJumpButtonPushed;
        InputHandler.JumpButtonReleased += ResetJumping;
        _timer.TimeIsUp -= OnTimerExpired;
    }


    private void Update()
    {
        if (_timer.IsActive)
            _timer.Tick();

        CheckIfGrounded();

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
        IsJumping = true;
        _timer.TimeIsUp += ResetJumping;
        _timer.StartTimer(0.5f);
    }

    public void ResetJumping() 
    {
        IsJumping = false;
        _timer.TimeIsUp -= ResetJumping;
    }

    private void CheckIfGrounded() 
    {
        IsGrounded = Physics2D.Raycast
            (transform.position, Vector2.down, PlayerData.GroundCheck, PlayerData.WallLayerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine (transform.position, 
            transform.position + Vector3.down * PlayerData.GroundCheck);
    }

    private void OnTimerExpired()  // test method for timer
    {
        Debug.Log("Time is up");
    }
}
