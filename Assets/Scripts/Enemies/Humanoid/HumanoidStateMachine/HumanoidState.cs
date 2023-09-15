using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanoidState : State
{
    public HumanoidStateMachine HumanoidStateMachine { get; protected set; }
    public Humanoid Humanoid { get; protected set; }
    public string AnimationCode { get; protected set; }

    public HumanoidState(HumanoidStateMachine humanoidStateMachine, string animationCode) 
    {
        HumanoidStateMachine = humanoidStateMachine;
        AnimationCode = animationCode;
        Humanoid = HumanoidStateMachine.Humanoid;
    }
    
    public override void Enter() 
    {
        Humanoid.Animator.Play(AnimationCode);
    }

    public override void Exit() {}

    public override State TryChange()
    {
        return null;
    }

    public override void UpdatePhysicalMotion() {}
}
