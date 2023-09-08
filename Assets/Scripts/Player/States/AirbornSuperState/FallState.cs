using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State
{
    private float _startXVelocity;
    private float _gravityAmplifier = 0.1f;

    public FallState(Player player, string animationCode) 
        : base(player, animationCode) {}

    public override void Enter()
    {
        base.Enter();
        _startXVelocity = Rigidbody.velocity.x;
    }

    public override State TryChange()
    {
        if (Player.Rigidbody.velocity.y < 0)
        {
            return this;
        }

        return null;
    }

    public override void UpdatePhysicalMotion()
    {
        float fallXModifer = 0.1f;

        if (InputHandler.MovementInput.x > 0)
        {
            _startXVelocity += fallXModifer;
        }
        else if (InputHandler.MovementInput.x < 0)
        {
            _startXVelocity -= fallXModifer;
        }       

        NextXVelocity = _startXVelocity;
        NextYVelocity = Rigidbody.velocity.y - _gravityAmplifier;
        base.UpdatePhysicalMotion();       
    }
}
