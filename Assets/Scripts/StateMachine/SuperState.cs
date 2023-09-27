public abstract class SuperState
{
    public StateMachine StateMachine { get; protected set; }
    public State DefaultState { get; protected set; }
    public State CurrentState { get; protected set; } 

    public SuperState(StateMachine stateMachine) => StateMachine = stateMachine;

    public virtual State SetState() 
    {
        State newState;

        newState = CurrentState.TryChange();

        if (newState != StateMachine.CurrentState)
        {
            StateMachine.CurrentState.Exit();
            newState.Enter();
            CurrentState = newState;
        }

        return newState;
    }
}