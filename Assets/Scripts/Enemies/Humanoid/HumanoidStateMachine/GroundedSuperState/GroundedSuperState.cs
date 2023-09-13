using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanoidNS
{
    public class GroundedSuperState : HumanoidSuperState
    {
        public SearchState SearchState { get; private set; } 
        
        public GroundedSuperState(HumanoidStateMachine stateMachine, Humanoid humanoid) : 
            base(stateMachine, humanoid) 
        {
            SearchState = new SearchState(stateMachine);
        }
    }
}
