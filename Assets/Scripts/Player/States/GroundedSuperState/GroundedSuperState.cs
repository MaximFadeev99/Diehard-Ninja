using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedSuperState : SuperState
{
    public AwakeState AwakeState { get; private set; }
    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public BlockState BlockState { get; private set; }
    public AttackState AttackState { get; private set; }

    public GroundedSuperState(StateMachine stateMachine) : base(stateMachine) 
    {
        AwakeState = new AwakeState(Player, "Awake");
        IdleState = new IdleState(Player, "Idle");
        RunState = new RunState(Player, "Run");
        JumpState = new JumpState(Player, "Jump");
        BlockState = new BlockState(Player, "Block");
        AttackState = new AttackState(Player, "Attack");
        DefaultState = AwakeState;
        CurrentState = DefaultState;
        CurrentState.Enter();
    }
      
    public override State SetState()
    {
        State newState = base.SetState();

        if (newState != JumpState)
            Player.InputHandler.ResetJumpInputBlock();

        return newState;
    }

    public void ChangeAwakeState() 
    {
        CurrentState = IdleState;
        IdleState.Enter();
    }
}
