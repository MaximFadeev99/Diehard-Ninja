using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    private float _xInput;

    public JumpState(Player player, string animationCode) 
        : base(player, animationCode) { }


    public override void Enter()
    {
        base.Enter();
        _xInput = InputHandler.MovementInput.x;       
    }

    public override State TryChange()
    {
        if (Player.IsJumping || Player.Rigidbody.velocity.y > 0f)
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
        float jumpForwardVelocityModifier = 7f;
        float jumpXVelocity = _xInput != 0f ?
               _xInput * jumpForwardVelocityModifier : Rigidbody.velocity.x;

        NextXVelocity = jumpXVelocity;
        NextYVelocity = Player.IsJumping ? PlayerData.JumpVelocity : Rigidbody.velocity.y;
        base.UpdatePhysicalMotion();
    }
}
