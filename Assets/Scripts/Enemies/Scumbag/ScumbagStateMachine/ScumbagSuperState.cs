public abstract class ScumbagSuperState : SuperState
{
    public Scumbag Scumbag { get; protected set; }

    public ScumbagSuperState(StateMachine stateMachine, Scumbag scumbag) : 
        base(stateMachine)
    {
        Scumbag = scumbag;
    }
}