using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DeflectState : State
{
    public DeflectState(Player player, string animationCode) 
        : base(player, animationCode) {}


    public override void Enter()
    {
        base.Enter();
        Player.ActivateInvincibilty();        
        //Debug.Log ("Start XVelocity: " +  _startXVelocity);
    }

    public override State TryChange()
    {
        if (Player.IsDeflecting)
        {
            return this;
        }
        else if (Player.IsDashing)
        {
            return Player.AbilitySuperState.DashState;
        }
        else 
        {
            return this;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        float xVelocityModifier = 7f;
        float xVelocity;

        if (InputHandler.MovementInput.x != 0) 
        {
            xVelocity = Player.IsFacingRight ? xVelocityModifier : xVelocityModifier * -1;
        }
        else
        {
            xVelocity = 0f;
        }

        NextXVelocity = xVelocity;
        NextYVelocity = Rigidbody.velocity.y;
        base.UpdatePhysicalMotion();
    }

    public override void Exit()
    {
        Player.DeactivateInvincibility();
    }
}
