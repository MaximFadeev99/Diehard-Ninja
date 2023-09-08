using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WallJumpState : State
{
    private float _xInput;
    private float _jumpXVelocity;

    public WallJumpState(Player player, string animationCode) : base(player, animationCode) {}

    public override void Enter()
    {
        base.Enter();
        _xInput = InputHandler.MovementInput.x;
    }

    public override State TryChange()
    {
        if (Player.IsWallJumping || Rigidbody.velocity.y > 0)
        {
            return this;
        }

        return null;
    }

    public override void UpdatePhysicalMotion()
    {
        float wallJumpVelocity = 7f;
        float wallJumpXVelocity;

        wallJumpXVelocity = Player.IsTouchingWallRight ? -wallJumpVelocity : wallJumpVelocity;
        NextXVelocity = wallJumpXVelocity;
        NextYVelocity = Player.IsWallJumping ? PlayerData.JumpVelocity : Rigidbody.velocity.y;
        base.UpdatePhysicalMotion();

        //if (Player.IsWallJumping)
        //{
        //    _jumpXVelocity = _xInput != 0f ?
        //        _xInput * jumpForwardVelocityModifier : Rigidbody.velocity.x;

        //    NextXVelocity = _jumpXVelocity;
        //    NextYVelocity = PlayerData.JumpVelocity;
        //    base.UpdatePhysicalMotion();
        //}
        //else
        //{
        //    NextXVelocity = _jumpXVelocity;
        //    NextYVelocity = Rigidbody.velocity.y;
        //    base.UpdatePhysicalMotion();
        //}
    }
}
