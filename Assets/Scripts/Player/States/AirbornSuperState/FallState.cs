using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State
{
    public FallState(Player player, string animationCode) 
        : base(player, animationCode) {}
    private float _startXVelocity;
    private float _gravityAmplifier = 0.1f;


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
        NextXVelocity = _startXVelocity;
        NextYVelocity = Rigidbody.velocity.y - _gravityAmplifier;
        base.UpdatePhysicalMotion();       
    }
}
