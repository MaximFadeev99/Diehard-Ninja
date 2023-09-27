public class AwakeState : PlayerState
{
    public AwakeState(PlayerStateMachine playerStateMachine, int animationCode) 
        : base(playerStateMachine, animationCode)  {}

    public override State TryChange() 
    {
        if (Player.IsAwakening) 
        {
            return this;
        }
        else 
        {
            return PlayerStateMachine.GroundedSuperState.IdleState;
        }
    }
}