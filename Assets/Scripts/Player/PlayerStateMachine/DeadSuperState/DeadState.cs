public class DeadState : PlayerState
{
    private float _deadYVelocity;
    
    public DeadState(PlayerStateMachine playerStateMachine, int animationCode) : 
        base(playerStateMachine, animationCode) {}

    public override State TryChange()
    {
        return this;
    }

    public override void UpdatePhysicalMotion()
    {
        float deadYVelocityModifier = -0.1f;
        
        NextXVelocity = 0;
        _deadYVelocity += deadYVelocityModifier;
        NextYVelocity = _deadYVelocity;
        base.UpdatePhysicalMotion();
    }
}