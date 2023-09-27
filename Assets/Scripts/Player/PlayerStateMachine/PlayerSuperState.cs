public abstract class PlayerSuperState : SuperState
{
    public Player Player { get; private set; }
    
    public PlayerSuperState(StateMachine stateMachine, Player player) : 
        base(stateMachine)
    {
        Player = player;
    }
}