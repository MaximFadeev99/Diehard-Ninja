using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeState : PlayerState
{
    public AwakeState(PlayerStateMachine playerStateMachine, string animationCode) 
        : base(playerStateMachine, animationCode)  {}

    public override State TryChange() 
    {
        return this;
    }
}
