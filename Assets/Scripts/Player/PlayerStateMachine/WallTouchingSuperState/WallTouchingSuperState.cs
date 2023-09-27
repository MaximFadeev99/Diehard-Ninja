public class WallTouchingSuperState : PlayerSuperState
{
    public WallSlidingState WallSlidingState { get; private set; } 
    public WallJumpState WallJumpState { get; private set; }
    
    public WallTouchingSuperState(PlayerStateMachine stateMachine, Player player) : 
        base(stateMachine, player)
    {
        WallSlidingState = new (stateMachine, AnimationData.WallSlide);
        WallJumpState = new (stateMachine, AnimationData.Jump);
        DefaultState = WallSlidingState;
        CurrentState = DefaultState;
    }

    public override State SetState()
    {
        State newState = base.SetState();

        if (newState != WallJumpState)
            Player.InputHandler.ResetJumpInputBlock();

        return newState;
    }
}