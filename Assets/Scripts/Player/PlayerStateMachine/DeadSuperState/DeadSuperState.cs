namespace PlayerNS
{
    public class DeadSuperState : PlayerSuperState
    {
        public DeadState DeadState { get; private set; }

        public DeadSuperState(PlayerStateMachine stateMachine, Player player) :
            base(stateMachine, player)
        {
            DeadState = new DeadState(stateMachine, AnimationData.Die);
            DefaultState = DeadState;
            CurrentState = DefaultState;
        }
    }
}
