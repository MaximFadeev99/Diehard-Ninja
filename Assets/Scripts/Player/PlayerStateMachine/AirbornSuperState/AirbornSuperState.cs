public class AirbornSuperState : PlayerSuperState
{
    public FallState FallState { get; private set; }

    public AirbornSuperState(PlayerStateMachine stateMachine, Player player) : 
        base(stateMachine, player)
    {
        FallState = new FallState(stateMachine, AnimationData.Fall);
        DefaultState = FallState;
        CurrentState = DefaultState;
    }
}