using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public SuperState CurrentSuperState { get; private set; }
    public State CurrentState { get; private set; }

    public Player Player { get; private set; }

    public StateMachine (Player player) 
    {
        Player = player;
    }

    public void DoLogicUpdate() 
    {
        SuperState newSuperState;

        if (Player.IsGrounded)
        {
            newSuperState = Player.GroundedSuperState;
            CurrentSuperState = newSuperState;
            CurrentState = newSuperState.SetState();
        }
        else  if (Player.IsGrounded == false && Player.Rigidbody.velocity.y < 0) 
        {
            newSuperState = Player.AirbornSuperState;
            CurrentSuperState = newSuperState;
            CurrentState = newSuperState.SetState();
        }

        //Debug.Log(CurrentSuperState + "+" + CurrentSuperState.CurrentState);
        //Debug.Log(CurrentSuperState.CurrentState + " + " + Player.Rigidbody.velocity);
    }

    public void DoPhysicsUpdate() => CurrentSuperState.CurrentState.UpdatePhysicalMotion();

    public void Reset() => CurrentSuperState = Player.GroundedSuperState;

    public void ExitAwakeState() 
    {
        CurrentSuperState = Player.GroundedSuperState;
        Player.GroundedSuperState.ChangeAwakeState();
    }
}
