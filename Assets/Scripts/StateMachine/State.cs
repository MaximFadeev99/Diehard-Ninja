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
    }

    public virtual void Enter()
    {
        Player.Animator.Play(AnimationCode);
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
        if (Player.InputHandler.MovementInput.x < 0)
        {
            Player.SpriteRenderer.flipX = true;
        }
        else
        {
            Player.SpriteRenderer.flipX = false;
        }
    }
}