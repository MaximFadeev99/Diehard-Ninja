using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public RunState(Player player, string animationCode) 
        : base(player, animationCode) {}

    public override State TryChange()
    {
        if (InputHandler.MovementInput.x == 0 && Player.IsJumping == false)
        {
            return Player.GroundedSuperState.IdleState;
        }
        else if (Player.IsJumping)
        {
            return Player.GroundedSuperState.JumpState;
        }
        else 
        {
            return this;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        FlipCharacter();

        NextXVelocity = InputHandler.MovementInput.x * 5;
        NextYVelocity = 0f;
        base.UpdatePhysicalMotion();     
    }
}
