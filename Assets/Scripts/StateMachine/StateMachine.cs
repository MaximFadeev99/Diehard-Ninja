public abstract class StateMachine 
{
    public SuperState CurrentSuperState { get; protected set; }
    public State CurrentState { get; protected set; }

    public abstract void DoLogicUpdate();
    public abstract void Reset();
    public void DoPhysicsUpdate() => CurrentSuperState.CurrentState.UpdatePhysicalMotion();
    protected void CallSuperState(SuperState newSuperState)
    {
        CurrentSuperState = newSuperState;
        CurrentState = CurrentSuperState.SetState();
    }
}