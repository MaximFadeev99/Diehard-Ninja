using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : PlayerState
{
    public BlockState(PlayerStateMachine playerStateMachine, string animationCode) : 
        base(playerStateMachine, animationCode) { }

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
            return PlayerStateMachine.GroundedSuperState.AttackState;
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
            return PlayerStateMachine.GroundedSuperState.IdleState;
        }
    }

    public override void Exit()
    {
        Player.DeactivateInvincibility();
        SpriteRenderer.flipX = !Player.IsFacingRight;
    }
}
