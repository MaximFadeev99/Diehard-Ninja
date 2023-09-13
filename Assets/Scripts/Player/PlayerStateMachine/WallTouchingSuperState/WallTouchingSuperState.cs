using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTouchingSuperState : PlayerSuperState
{
    public WallSlidingState WallSlidingState { get; private set; } 
    public WallJumpState WallJumpState { get; private set; }
    
    public WallTouchingSuperState(PlayerStateMachine stateMachine, Player player) : 
        base(stateMachine, player)
    {
        WallSlidingState = new WallSlidingState(stateMachine, "WallSlide");
        WallJumpState = new WallJumpState(stateMachine, "Jump");
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
