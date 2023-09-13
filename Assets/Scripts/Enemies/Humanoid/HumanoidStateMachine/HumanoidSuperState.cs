using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanoidSuperState : SuperState
{
    public Humanoid Humanoid { get; protected set; }

    public HumanoidSuperState(StateMachine stateMachine, Humanoid humanoid) : 
        base(stateMachine)
    {
        Humanoid = humanoid;
    }
}
