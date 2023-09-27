using PlayerNS;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }
    public GroundedSuperState GroundedSuperState { get; private set; }
    public AirbornSuperState AirbornSuperState { get; private set; }
    public WallTouchingSuperState WallTouchingSuperState { get; private set; }
    public AbilitySuperState AbilitySuperState { get; private set; }
    public DeadSuperState DeadSuperState { get; private set; }

    public PlayerStateMachine (Player player) 
    {
        Player = player;
        GroundedSuperState = new (this, Player);
        AirbornSuperState = new (this, Player);
        WallTouchingSuperState = new (this, Player);
        AbilitySuperState = new (this, Player);
        DeadSuperState = new (this, Player);
    }

    public override void DoLogicUpdate() 
    {
        if (Player.IsDead) 
        {
            CallSuperState(DeadSuperState);
        }
        else if (Player.IsUsingAbility)
        {
            CallSuperState(AbilitySuperState);
        }
        else if (Player.IsGrounded || Player.IsJumping)
        {
            CallSuperState(GroundedSuperState);
        }
        else if (Player.IsTouchingWallLeft || Player.IsTouchingWallRight || Player.IsWallJumping)
        {
            CallSuperState(WallTouchingSuperState);
        }
        else if (Player.Rigidbody.velocity.y < 0)
        {
            CallSuperState(AirbornSuperState);
        }
    }

    public override void Reset()
    {
        CurrentSuperState = GroundedSuperState;
        CurrentState = GroundedSuperState.AwakeState;
    } 
}
