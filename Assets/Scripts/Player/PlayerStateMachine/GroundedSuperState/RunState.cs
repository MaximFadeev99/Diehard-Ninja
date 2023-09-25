using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerState
{
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    public RunState(PlayerStateMachine playerStateMachine, string animationCode) 
        : base(playerStateMachine, animationCode)     
    {
        _audioSource = Player.AudioSource;
        _audioClip = Player.PlayerData.RuninngSound;
    }

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
            return this;
        }
        else
        {
            return PlayerStateMachine.GroundedSuperState.IdleState;
        }
    }

    public override void Enter()
    {
        base.Enter();
        _audioSource.clip = _audioClip;
        _audioSource.volume = 1f;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public override void UpdatePhysicalMotion()
    {
        FlipCharacter();
        NextXVelocity = InputHandler.MovementInput.x * PlayerData.RunSpeed;
        NextYVelocity = 0f;
        base.UpdatePhysicalMotion();     
    }

    public override void Exit()
    {
        _audioSource.Stop();
    }
}
