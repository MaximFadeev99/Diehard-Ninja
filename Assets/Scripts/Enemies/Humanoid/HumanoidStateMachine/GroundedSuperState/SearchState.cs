using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SearchState : HumanoidState
{
    private SearchArea _searchArea;
    private HumanoidData _humanoidData;
    private Tween _headMovement;
    private float _headRotation = 7f;
    private Tween _handMovement;

    public Action<bool> PlayerGotBehind;
    
    public SearchState(HumanoidStateMachine humanoidStateMachine, string animationCode) : base(humanoidStateMachine, animationCode)
    {
        _searchArea = Humanoid.SearchArea;
        _humanoidData = Humanoid.HumanoidData;
        _searchArea.Set(Humanoid,_humanoidData.SearchAreaWidth, _humanoidData.SearchAreaHeight);
        _headMovement = Humanoid.Head.transform.DOLocalRotate(new Vector3(0f, 0f, _headRotation), 1f);
        _headMovement.SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public override void Enter()
    {
        Humanoid.RightHand.gameObject.SetActive(false);
        _headMovement.Play();
    }

    public override State TryChange()
    {
        //_searchArea.LookForPlayer();       

        if (Humanoid.Player == null)
        {
            return this;
        }
        else 
        {
            return HumanoidStateMachine.GroundedSuperState.AttackState;
        }
    }

    public override void Exit()
    {
        _headMovement.Pause();
    }
}
