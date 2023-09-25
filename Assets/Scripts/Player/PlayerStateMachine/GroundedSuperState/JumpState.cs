using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    private float _xInput;
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    public JumpState(PlayerStateMachine playerStateMachine, string animationCode) 
        : base(playerStateMachine, animationCode) 
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
    }

    public override State TryChange()
    {
        if (Player.IsJumping || Player.Rigidbody.velocity.y > 0f)
        {
            return this;
        }
        else
        {
            return PlayerStateMachine.GroundedSuperState.IdleState;
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
