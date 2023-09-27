namespace ScumbagNS
{   
    public class DeadSuperState : ScumbagSuperState
    {
        public DeadState DeadState { get; private set; }

        public DeadSuperState(ScumbagStateMachine stateMachine, Scumbag scumbag) 
            : base(stateMachine, scumbag)
        {
            DeadState = new DeadState(stateMachine);
            DefaultState = DeadState;
            CurrentState = DefaultState;
        }
    }
}