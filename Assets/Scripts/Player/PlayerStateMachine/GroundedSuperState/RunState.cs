using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerStateMachine playerStateMachine, int animationCode) 
        : base(playerStateMachine, animationCode)     
    {
        AudioClip = Player.PlayerData.RuninngSound;
    }

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
            return this;
        }
        else
        {
            return PlayerStateMachine.GroundedSuperState.IdleState;
        }
    }

    public override void Enter()
    {
        base.Enter();
        PlaySound(1f, true);
    }

    public override void UpdatePhysicalMotion()
    {
        FlipCharacter();
        NextXVelocity = InputHandler.MovementInput.x * PlayerData.RunSpeed;
        NextYVelocity = 0f;
        base.UpdatePhysicalMotion();     
    }

    public override void Exit() => AudioSource.Stop();
}