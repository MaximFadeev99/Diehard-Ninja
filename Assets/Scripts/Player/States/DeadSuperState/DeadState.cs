using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    private float _deadYVelocity;
    
    public DeadState(Player player, string animationCode) : 
        base(player, animationCode) {}

    public override State TryChange()
    {
        return this;
    }

    public override void UpdatePhysicalMotion()
    {
        float deadYVelocityModifier = -0.1f;
        
        _deadYVelocity += deadYVelocityModifier;
        NextXVelocity = 0;
        NextYVelocity = _deadYVelocity;
        base.UpdatePhysicalMotion();
    }
}
