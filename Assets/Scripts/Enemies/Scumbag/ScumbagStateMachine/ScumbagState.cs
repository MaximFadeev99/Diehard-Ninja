public abstract class ScumbagState : State
{
    public ScumbagStateMachine ScumbagStateMachine { get; protected set; }
    public Scumbag Scumbag { get; protected set; }

    public ScumbagState(ScumbagStateMachine scumbagStateMachine) 
    {
        ScumbagStateMachine = scumbagStateMachine;
        Scumbag = ScumbagStateMachine.Scumbag;
    }
    
    public override void Enter() {}

    public override void Exit() {}

    public override void UpdatePhysicalMotion() {}
}