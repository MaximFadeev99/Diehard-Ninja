using UnityEngine;

public class FallState : PlayerState
{
    private float _startXVelocity;
    
    public FallState(PlayerStateMachine playerStateMachine,int animationCode) 
        : base(playerStateMachine, animationCode) {}

    public override void Enter()
    {
        base.Enter();
        _startXVelocity = Rigidbody.velocity.x;
        Rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    public override State TryChange() 
    {  
        return this;
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

    public override void Exit() => 
        Rigidbody.interpolation = RigidbodyInterpolation2D.None;
}