using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    public BlockState(Player player, string animationCode) : base(player, animationCode) { }

    public override void Enter()
    {
        SpriteRenderer.flipX = Player.IsFacingRight;
        base.Enter();
        Player.ActivateInvincibilty();
    }

    public override State TryChange()
    {
        if (Player.IsBlocking)
        {
            return this;
        }
        else if (Player.IsAttacking) 
        {
            return Player.GroundedSuperState.AttackState;
        }
        //else if (Player.IsJumping)
        //{
        //    return Player.GroundedSuperState.JumpState;
        //}
        //else if (InputHandler.MovementInput.x != 0 && Player.IsJumping == false)
        //{
        //    return Player.GroundedSuperState.RunState;
        //}
        else
        {
            return Player.GroundedSuperState.IdleState;
        }
    }

    public override void Exit()
    {
        Player.DeactivateInvincibility();
        SpriteRenderer.flipX = !Player.IsFacingRight;
    }
}
