public class WallJumpState : PlayerState
{
    private bool _wasTouchingWallRight;

    public WallJumpState(PlayerStateMachine playerStateMachine, int animationCode) :
        base(playerStateMachine, animationCode) => AudioClip = Player.Data.JumpingSound;

    public override void Enter()
    {
        base.Enter();
        PlaySound(0.4f, false);
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
            return PlayerStateMachine.WallTouchingSuperState.WallSlidingState;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        float wallJumpXVelocity = _wasTouchingWallRight ? -7f : 7f;

        Player.IsFacingRight = !_wasTouchingWallRight;
        NextXVelocity = wallJumpXVelocity;
        NextYVelocity = Player.IsWallJumping ? PlayerData.JumpVelocity : Rigidbody.velocity.y;
        base.UpdatePhysicalMotion();
        Player.ResumeRaycasts();
    }
}