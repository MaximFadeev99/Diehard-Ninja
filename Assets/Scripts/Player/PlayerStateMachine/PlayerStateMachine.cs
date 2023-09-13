using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNS;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; private set; }
    public GroundedSuperState GroundedSuperState { get; private set; }
    public AirbornSuperState AirbornSuperState { get; private set; }
    public WallTouchingSuperState WallTouchingSuperState { get; private set; }
    public AbilitySuperState AbilitySuperState { get; private set; }
    public DeadSuperState DeadSuperState { get; private set; }

    public PlayerStateMachine (Player player) 
    {
        Player = player;
        GroundedSuperState = new GroundedSuperState(this, Player);
        AirbornSuperState = new AirbornSuperState(this, Player);
        WallTouchingSuperState = new WallTouchingSuperState(this, Player);
        AbilitySuperState = new AbilitySuperState(this, Player);
        DeadSuperState = new DeadSuperState(this, Player);
    }

    public override void DoLogicUpdate() 
    {
        if (Player.IsDead) 
        {
            ChangeSuperState(DeadSuperState);
        }
        else if (Player.IsUsingAbility)
        {
            ChangeSuperState(AbilitySuperState);
        }
        else if (Player.IsGrounded || Player.IsJumping)
        {
            ChangeSuperState(GroundedSuperState);
        }
        else if (Player.IsTouchingWallLeft || Player.IsTouchingWallRight || Player.IsWallJumping)
        {
            ChangeSuperState(WallTouchingSuperState);
        }
        else if (Player.Rigidbody.velocity.y < 0)
        {
            ChangeSuperState(AirbornSuperState);
        }
    }

    public override void Reset()
    {
        CurrentSuperState = GroundedSuperState;
        CurrentState = GroundedSuperState.AwakeState;
    } 

    public void ExitAwakeState() 
    {
        CurrentSuperState = GroundedSuperState;
        GroundedSuperState.ChangeAwakeState();
    }
}
