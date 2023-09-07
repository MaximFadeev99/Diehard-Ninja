using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedSuperState : SuperState
{
    public AwakeState AwakeState { get; private set; }
    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public bool isJumping { get; protected set; } = false;

    public GroundedSuperState(StateMachine stateMachine) : base(stateMachine) 
    {
        AwakeState = new AwakeState(Player, "Awake");
        IdleState = new IdleState(Player, "Idle");
        RunState = new RunState(Player, "Run");
        JumpState = new JumpState(Player, "Jump");
        DefaultState = AwakeState;
        EnterDefaultState();
    }
      
    public override State SetState()
    {
        State newState;

        newState = CurrentState.TryChange();

        if (newState != StateMachine.CurrentState)
        {
            newState.Enter();
            CurrentState = newState;
        }

        return newState;

    }

    public void ChangeAwakeState() 
    {
        CurrentState = IdleState;
        IdleState.Enter();
    }
}
