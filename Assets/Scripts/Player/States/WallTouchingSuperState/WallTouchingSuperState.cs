using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTouchingSuperState : SuperState
{
    public WallSlidingState WallSlidingState { get; private set; } 
    public WallJumpState WallJumpState { get; private set; }
    
    public WallTouchingSuperState(StateMachine stateMachine) : base(stateMachine)
    {
        WallSlidingState = new WallSlidingState(Player, "WallSlide");
        WallJumpState = new WallJumpState(Player, "Jump");
        DefaultState = WallSlidingState;
        CurrentState = DefaultState;
    }

    public override State SetState()
    {
        State newState = base.SetState();

        if (newState != WallJumpState)
            Player.InputHandler.ResetJumpInputBlock();

        return newState;
    }
}
