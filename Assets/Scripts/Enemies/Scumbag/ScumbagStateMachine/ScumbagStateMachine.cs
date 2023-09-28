using ScumbagNS;

public class ScumbagStateMachine : StateMachine
{
    public Scumbag Scumbag { get; private set; }
    public GroundedSuperState GroundedSuperState { get; private set; }
    public DeadSuperState DeadSuperState { get; private set; }

    public ScumbagStateMachine(Scumbag scumbag)
    {
        Scumbag = scumbag;
        GroundedSuperState = new (this, Scumbag);
        DeadSuperState = new (this, Scumbag);
        Reset();
    }

    public override void DoLogicUpdate()
    {
        if (Scumbag.IsDead)
        {
            CallSuperState(DeadSuperState);
        }
        else
        {
            CallSuperState(GroundedSuperState);
        }
    }

    public override void Reset()
    {
        CurrentSuperState = GroundedSuperState;
        CurrentState = GroundedSuperState.SearchState;
        CurrentState.Enter();
    }
}