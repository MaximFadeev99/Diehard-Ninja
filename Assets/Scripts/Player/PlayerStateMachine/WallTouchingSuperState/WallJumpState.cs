using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;

public class WallJumpState : PlayerState
{
    private float _xInput;
    private float _jumpXVelocity;
    private bool _wasTouchingWallRight;
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    public WallJumpState(PlayerStateMachine playerStateMachine, string animationCode) :
        base(playerStateMachine, animationCode) 
    {
        _audioSource = Player.AudioSource;
        _audioClip = Player.PlayerData.JumpingSound;
    }

    public override void Enter()
    {
        base.Enter();
        _audioSource.clip = _audioClip;
        _audioSource.volume = 0.4f;
        _audioSource.loop = false;
        _audioSource.Play();
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
            return PlayerStateMachine.WallTouchingSuperState.WallSlidingState;
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
