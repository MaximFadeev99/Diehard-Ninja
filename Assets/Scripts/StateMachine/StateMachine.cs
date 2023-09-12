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
        if (Player.IsDead) 
        {
            ChangeSuperState(Player.DeadSuperState);
        }
        else if (Player.IsUsingAbility)
        {
            ChangeSuperState(Player.AbilitySuperState);
        }
        else if (Player.IsGrounded || Player.IsJumping)
        {
            ChangeSuperState(Player.GroundedSuperState);
        }
        else if (Player.IsTouchingWallLeft || Player.IsTouchingWallRight || Player.IsWallJumping)
        {
            ChangeSuperState(Player.WallTouchingSuperState);
        }
        else if (Player.Rigidbody.velocity.y < 0)
        {
            ChangeSuperState(Player.AirbornSuperState);
        }

        //Debug.Log(CurrentSuperState + "+" + CurrentSuperState.CurrentState);
        //Debug.Log(CurrentSuperState.CurrentState + " + " + Player.Rigidbody.velocity);
    }

    public void DoPhysicsUpdate() => CurrentSuperState.CurrentState.UpdatePhysicalMotion();

    public void Reset()
    {
        CurrentSuperState = Player.GroundedSuperState;
        CurrentState = Player.GroundedSuperState.AwakeState;
    } 

    public void ExitAwakeState() 
    {
        CurrentSuperState = Player.GroundedSuperState;
        Player.GroundedSuperState.ChangeAwakeState();
    }

    private void ChangeSuperState(SuperState newSuperState) 
    {
        CurrentSuperState = newSuperState;
        CurrentState = CurrentSuperState.SetState();
    }
}
