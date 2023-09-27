namespace ScumbagNS
{
    public class GroundedSuperState : ScumbagSuperState
    {
        public SearchState SearchState { get; private set; } 
        public AttackState AttackState { get; private set; }
        
        public GroundedSuperState(ScumbagStateMachine stateMachine, Scumbag scumbag) : 
            base(stateMachine, scumbag) 
        {
            SearchState = new (stateMachine);
            AttackState = new (stateMachine);
            DefaultState = SearchState;
            CurrentState = DefaultState;
        }
    }
}