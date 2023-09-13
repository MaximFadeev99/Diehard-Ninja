using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : PlayerState
{
    private float _startXVelocity;
    

    public FallState(PlayerStateMachine playerStateMachine,string animationCode) 
        : base(playerStateMachine, animationCode) {}

    public override void Enter()
    {
        base.Enter();
        _startXVelocity = Rigidbody.velocity.x;
        //Debug.Log("Start XVelocity: " + _startXVelocity);
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
        float gravityAmplifier = 0.1f;

        if (InputHandler.MovementInput.x > 0)
        {
            _startXVelocity += fallXModifer;
        }
        else if (InputHandler.MovementInput.x < 0)
        {
            _startXVelocity -= fallXModifer;
        }       

        NextXVelocity = _startXVelocity;
        NextYVelocity = Rigidbody.velocity.y - gravityAmplifier;
        base.UpdatePhysicalMotion();       
    }
}
