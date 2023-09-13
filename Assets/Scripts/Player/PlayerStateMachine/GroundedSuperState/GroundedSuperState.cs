using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerNS
{
    public class GroundedSuperState : PlayerSuperState
    {
        public AwakeState AwakeState { get; private set; }
        public IdleState IdleState { get; private set; }
        public RunState RunState { get; private set; }
        public JumpState JumpState { get; private set; }
        public BlockState BlockState { get; private set; }
        public AttackState AttackState { get; private set; }

        public GroundedSuperState(PlayerStateMachine stateMachine, Player player) :
            base(stateMachine, player)
        {
            AwakeState = new AwakeState(stateMachine, "Awake");
            IdleState = new IdleState(stateMachine, "Idle");
            RunState = new RunState(stateMachine, "Run");
            JumpState = new JumpState(stateMachine, "Jump");
            BlockState = new BlockState(stateMachine, "Block");
            AttackState = new AttackState(stateMachine, "Attack");
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
}
