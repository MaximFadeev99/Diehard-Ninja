using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeState : State
{
    public AwakeState(Player player, string animationCode) 
        : base(player, animationCode)  {}

    public override State TryChange() 
    {
        return this;
    }
}
