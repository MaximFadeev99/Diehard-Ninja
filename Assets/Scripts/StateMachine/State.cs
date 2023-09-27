public abstract class State
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract State TryChange();

    public abstract void UpdatePhysicalMotion();
}