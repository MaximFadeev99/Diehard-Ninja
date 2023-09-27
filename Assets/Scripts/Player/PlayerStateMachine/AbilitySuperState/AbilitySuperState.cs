public class AbilitySuperState : PlayerSuperState
{
    public DeflectState DeflectState;
    public DashState DashState;

    public AbilitySuperState(PlayerStateMachine stateMachine, Player player) : 
        base(stateMachine, player)
    {
        DeflectState = new DeflectState(stateMachine, AnimationData.Deflect);
        DashState = new DashState(stateMachine, AnimationData.Dash);
        DefaultState = DeflectState;
        CurrentState = DefaultState;
    }
}