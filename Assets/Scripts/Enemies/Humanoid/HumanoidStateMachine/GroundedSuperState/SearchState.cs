using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : HumanoidState
{
    private SearchArea _searchArea;
    private HumanoidData _humanoidData;
    
    public SearchState(HumanoidStateMachine humanoidStateMachine, string animationCode) : base(humanoidStateMachine, animationCode)
    {
        _searchArea = humanoidStateMachine.Humanoid.SearchArea;
        _humanoidData = humanoidStateMachine.Humanoid.HumanoidData;
        _searchArea.Set(humanoidStateMachine.Humanoid,_humanoidData.SearchAreaWidth, _humanoidData.SearchAreaHeight);
    }

    public override State TryChange()
    {
        _searchArea.LookForPlayer();       

        if (HumanoidStateMachine.Humanoid.Player == null)
        {
            return this;
        }
        else 
        {
            return HumanoidStateMachine.GroundedSuperState.AttackState;
        }
    }
}
