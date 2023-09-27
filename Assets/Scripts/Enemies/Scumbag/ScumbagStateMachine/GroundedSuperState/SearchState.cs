using UnityEngine;
using DG.Tweening;
using System;

public class SearchState : ScumbagState
{
    private SearchArea _searchArea;
    private ScumbagData _scumbagData;
    private Tween _headMovement;
    private float _headRotation = 7f;

    public Action<bool> PlayerGotBehind;
    
    public SearchState(ScumbagStateMachine scumbagStateMachine) : base(scumbagStateMachine)
    {
        _searchArea = Scumbag.SearchArea;
        _scumbagData = Scumbag.ScumbagData;
        _searchArea.Set(Scumbag,_scumbagData.SearchAreaWidth, _scumbagData.SearchAreaHeight);
        _headRotation = Scumbag.IsFacingRight ? _headRotation : -_headRotation;
        _headMovement = Scumbag.Head.transform.DOLocalRotate(new Vector3(0f, 0f, _headRotation), 1f);
        _headMovement.SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public override void Enter()
    {
        Scumbag.RightHand.gameObject.SetActive(false);
        _headMovement.Play();
    }

    public override State TryChange()
    {      
        if (Scumbag.IsPlayerInRange == false || Scumbag.Player.IsDead)
        {
            return this;
        }
        else 
        {
            return ScumbagStateMachine.GroundedSuperState.AttackState;
        }
    }

    public override void Exit() => _headMovement.Pause();
}