using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanoidNS
{
    public class AttackState : HumanoidState
    {

        public AttackState(HumanoidStateMachine humanoidStateMachine, string animationCode) : base(humanoidStateMachine, animationCode)
        {
            //_player = humanoidStateMachine.Humanoid.Player;
        }

        public override void Enter()
        {          
            base.Enter();
            //Humanoid.LeftHand.gameObject.SetActive(Humanoid.WeaponData._isLeftHandVisible);
        }

        public override State TryChange()
        {
            if (HumanoidStateMachine.Humanoid.Player != null)
            {               
                return this;
            }
            else 
            {
                return HumanoidStateMachine.GroundedSuperState.SearchState;
            }
        }
    }






}
