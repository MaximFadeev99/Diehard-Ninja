using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;

public class WallJumpState : State
{
    private float _xInput;
    private float _jumpXVelocity;
    private bool _wasTouchingWallRight;

    public WallJumpState(Player player, string animationCode) : base(player, animationCode) {}

    public override void Enter()
    {
        base.Enter();
        _xInput = InputHandler.MovementInput.x;
        _wasTouchingWallRight = Player.IsTouchingWallRight;
        Player.AnnulAndPauseWallRaycats();
    }

    public override State TryChange()
    {
        if (Player.IsWallJumping)
        {
            return this;
        }
        else 
        {
            return Player.WallTouchingSuperState.WallSlidingState;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        float wallJumpVelocity = 7f;
        float wallJumpXVelocity;

        wallJumpXVelocity = _wasTouchingWallRight ? -wallJumpVelocity : wallJumpVelocity;
        Player.IsFacingRight = _wasTouchingWallRight ? false : true;
        NextXVelocity = wallJumpXVelocity;
        NextYVelocity = Player.IsWallJumping ? PlayerData.JumpVelocity : Rigidbody.velocity.y;
        base.UpdatePhysicalMotion();
        Player.ResumeRaycasts();
    }
}
