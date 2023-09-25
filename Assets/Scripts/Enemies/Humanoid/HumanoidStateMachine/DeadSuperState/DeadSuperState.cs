using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanoidNS
{   
    public class DeadSuperState : HumanoidSuperState
    {
        public DeadState DeadState { get; private set; }

        public DeadSuperState(HumanoidStateMachine stateMachine, Humanoid humanoid) 
            : base(stateMachine, humanoid)
        {
            DeadState = new DeadState(stateMachine, string.Empty);
            DefaultState = DeadState;
            CurrentState = DefaultState;
        }
    }
}
