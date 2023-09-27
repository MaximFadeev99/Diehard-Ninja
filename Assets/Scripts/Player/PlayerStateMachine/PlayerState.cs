using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerState : State
{
    protected PlayerStateMachine PlayerStateMachine;
    protected Player Player;
    protected Animator Animator;
    protected Rigidbody2D Rigidbody;
    protected PlayerInputHandler InputHandler;
    protected PlayerData PlayerData;
    protected SpriteRenderer SpriteRenderer;
    protected AudioSource AudioSource;
    protected AudioClip AudioClip;
    protected int AnimationCode;
    protected float NextXVelocity;
    protected float NextYVelocity;

    public PlayerState (PlayerStateMachine playerStateMachine, int animationCode)
    {
        PlayerStateMachine = playerStateMachine;
        AnimationCode = animationCode;
        Player = PlayerStateMachine.Player;
        Animator = Player.Animator;
        Rigidbody = Player.Rigidbody;
        InputHandler = Player.InputHandler;
        PlayerData = Player.PlayerData;
        SpriteRenderer = Player.SpriteRenderer;
        AudioSource = Player.AudioSource;
    }

    public override void Enter()
    {
        Player.Animator.Play(AnimationCode);
    }

    public override void Exit() {}

    public override void UpdatePhysicalMotion() 
    {
        Rigidbody.velocity = new (NextXVelocity, NextYVelocity);
        NextXVelocity = Rigidbody.velocity.x;
        NextYVelocity = Rigidbody.velocity.y;
    }

    public void FlipCharacter() 
    {
        if (InputHandler.MovementInput.x < 0)
        {
            Player.SpriteRenderer.flipX = true;
            Player.IsFacingRight = false;
        }
        else
        {
            Player.SpriteRenderer.flipX = false;
            Player.IsFacingRight = true;
        }
    }

    protected void PlaySound(float volume, bool isLooping) 
    {
        AudioSource.clip = AudioClip;
        AudioSource.volume = volume;
        AudioSource.loop = isLooping;
        AudioSource.Play();
    }
}