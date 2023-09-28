namespace PlayerNS
{
    public class GroundedSuperState : PlayerSuperState
    {
        public AwakeState AwakeState { get; private set; }
        public IdleState IdleState { get; private set; }
        public RunState RunState { get; private set; }
        public JumpState JumpState { get; private set; }
        public BlockState BlockState { get; private set; }
        public AttackState AttackState { get; private set; }

        public GroundedSuperState(PlayerStateMachine stateMachine, Player player) :
            base(stateMachine, player)
        {
            AwakeState = new (stateMachine, AnimationData.Awake);
            IdleState = new (stateMachine, AnimationData.Idle);
            RunState = new (stateMachine, AnimationData.Run);
            JumpState = new (stateMachine, AnimationData.Jump);
            BlockState = new (stateMachine, AnimationData.Block);
            AttackState = new (stateMachine, 0);
            DefaultState = AwakeState;
            CurrentState = DefaultState;
            CurrentState.Enter();
        }

        public override State SetState()
        {
            State newState = base.SetState();

            if (newState != JumpState)
                Player.InputHandler.ResetJumpInputBlock();

            return newState;
        }

        public void ChangeAwakeState()
        {
            CurrentState = IdleState;
            IdleState.Enter();
        }
    }
}