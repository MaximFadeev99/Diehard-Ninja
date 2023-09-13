using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine playerStateMachine, string animationCode) 
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
        else if (InputHandler.MovementInput.x != 0 && Player.IsJumping == false)
        {
            return PlayerStateMachine.GroundedSuperState.RunState;
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
