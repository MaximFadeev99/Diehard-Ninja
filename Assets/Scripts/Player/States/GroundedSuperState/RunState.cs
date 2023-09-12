using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public RunState(Player player, string animationCode) 
        : base(player, animationCode) {}

    public override State TryChange()
    {
        if (Player.IsBlocking)
        {
            return Player.GroundedSuperState.BlockState;
        }
        else if (Player.IsAttacking) 
        {
            return Player.GroundedSuperState.AttackState;
        }
        else if (Player.IsJumping)
        {
            return Player.GroundedSuperState.JumpState;
        }
        else if (InputHandler.MovementInput.x != 0 && Player.IsJumping == false)
        {
            return this;
        }
        else
        {
            return Player.GroundedSuperState.IdleState;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        FlipCharacter();
        NextXVelocity = InputHandler.MovementInput.x * PlayerData.RunSpeed;
        NextYVelocity = 0f;
        base.UpdatePhysicalMotion();     
    }
}
