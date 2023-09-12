using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State
{    
    protected string AnimationCode;
    protected Player Player;
    protected Animator Animator;
    protected Rigidbody2D Rigidbody;
    protected PlayerInputHandler InputHandler;
    protected PlayerData PlayerData;
    protected SpriteRenderer SpriteRenderer;
    protected float NextXVelocity;
    protected float NextYVelocity;

    public State(Player player, string animationCode)
    {
        AnimationCode = animationCode;
        Player = player;
        Animator = Player.Animator;
        Rigidbody = Player.Rigidbody;
        InputHandler = Player.InputHandler;
        PlayerData = Player.PlayerData;
        SpriteRenderer = Player.SpriteRenderer;
    }

    public virtual void Enter()
    {
        Player.Animator.Play(AnimationCode);
    }

    public virtual void Exit() 
    {
        //Player.Animator.StopPlayback();
    }

    public abstract State TryChange();

    public virtual void UpdatePhysicalMotion() 
    {
        Rigidbody.velocity = new Vector2(NextXVelocity, NextYVelocity);
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

       // Debug.Log("IsFacingRight: " + Player.IsFacingRight);
    }
}