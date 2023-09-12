using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Player player, string animationCode) 
        : base(player, animationCode) { }

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
            return Player.GroundedSuperState.RunState;
        }
        else
        {
            return this;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        base.UpdatePhysicalMotion();
    }
}
