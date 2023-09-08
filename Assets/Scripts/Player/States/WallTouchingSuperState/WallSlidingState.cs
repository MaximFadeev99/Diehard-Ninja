using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlidingState : State
{
    public WallSlidingState(Player player, string animationCode) 
        : base(player, animationCode){}

    public override void Enter()
    {
        base.Enter();
        MoveCloserToWall();
    }

    public override State TryChange()
    {
        if (Player.IsWallJumping)
        {
            return Player.WallTouchingSuperState.WallJumpState;
        }
        else 
        {
            return this;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        SpriteRenderer.flipX = Player.IsTouchingWallRight;       
        NextXVelocity = 0;
        NextYVelocity = -PlayerData.WallSlideSpeed;
        base.UpdatePhysicalMotion();
    }

    private void MoveCloserToWall() 
    {
        float correction = 0.15f;
        Vector2 correctedPosition;

        correctedPosition = Player.IsTouchingWallRight ?
            new Vector2(Player.transform.position.x + correction, Player.transform.position.y) :
            new Vector2(Player.transform.position.x - correction, Player.transform.position.y);
        Player.transform.position = correctedPosition;
    }
}
