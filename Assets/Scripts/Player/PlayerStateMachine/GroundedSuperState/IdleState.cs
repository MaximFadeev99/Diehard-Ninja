public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine playerStateMachine, int animationCode) 
        : base(playerStateMachine, animationCode) { }

    public override State TryChange()
    {
        if (Player.IsBlocking)
        {
            return PlayerStateMachine.GroundedSuperState.BlockState;
        }
        else if (Player.IsAttacking) 
        {
            return PlayerStateMachine.GroundedSuperState.AttackState;
        }
        else if (Player.IsJumping)
        {
            return PlayerStateMachine.GroundedSuperState.JumpState;
        }
        else if (InputHandler.MovementInput.x != 0)
        {
            return PlayerStateMachine.GroundedSuperState.RunState;
        }
        else
        {
            return this;
        }
    }
}