using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchArea : MonoBehaviour // TODO: Remove alert area if unrequired
{
    public BoxCollider2D BoxCollider2D { get; private set; }

    private Humanoid _humanoid;
    private Player _player;
    private float _alertArea;
    private bool _isRaycastRequired = false;
    private bool _isCheckingFlip;
    private ContactFilter2D _contactFilter;
    //private bool _isFacingRight;
    private float _offsetModifier = 0.7f;
    private float _currentOffsetModifier;

    public Action<bool> PlayerGotBehind;

    public void Update()
    {
        if (_isRaycastRequired)
            DoRaycast();

        if (_humanoid.IsPlayerInRange) 
            CheckIfFlipRequired();   
    }

    public void Set(Humanoid humanoid, float searchAreaWidth, float searchAreaHight) 
    {

        _humanoid = humanoid;
        _player = _humanoid.Player;
        //_isFacingRight = isFacingRight;
        BoxCollider2D = GetComponent<BoxCollider2D>();
        BoxCollider2D.isTrigger = true;
        BoxCollider2D.size = new Vector2(searchAreaWidth, searchAreaHight);
        _currentOffsetModifier = _humanoid.IsFacingRightFirst ? _offsetModifier : -_offsetModifier;
        BoxCollider2D.offset = new Vector2(BoxCollider2D.size.x / 2f * _currentOffsetModifier, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player)) 
        {
            _isRaycastRequired = true;
            _isCheckingFlip = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player)) 
        {
            _isRaycastRequired = false;
            _isCheckingFlip = false;
            _humanoid.SetPlayerInRange(false);
        } 
    }

    private void DoRaycast() 
    {
        //bool isPlayerRight;
        int playerLayer = 8;
        int wallLayer = 6;
        Vector2 raycastDirection = (Vector2)_player.transform.position - (Vector2)transform.position; 
        int maxHitCount = 7;
        RaycastHit2D[] _hits = new RaycastHit2D[maxHitCount];

        Physics2D.Raycast(transform.position, raycastDirection, _contactFilter.NoFilter(), 
            _hits, BoxCollider2D.size.x);

        for (int i = 0; i < _hits.Length; i++) 
        {
            if (_hits[i].collider.gameObject.layer == wallLayer)
            {
                return;
            }
            else if (_hits[i].collider.gameObject.layer == playerLayer) 
            {
                _isRaycastRequired = false;
                //isPlayerRight = _player.transform.position.x >= transform.position.x ? true : false;
                _humanoid.SetPlayerInRange(true);
                //print("Player is found and set");
                return;
            }
        }       
    }

    private void CheckIfFlipRequired() 
    {
        if ((_humanoid.IsFacingRight && _player.transform.position.x < transform.position.x) ||
            (_humanoid.IsFacingRight == false && _player.transform.position.x > transform.position.x))
        {
            _currentOffsetModifier = -_currentOffsetModifier;
            BoxCollider2D.offset = new Vector2(BoxCollider2D.size.x / 2f * _currentOffsetModifier, 0f);
            PlayerGotBehind.Invoke(!_humanoid.IsFacingRight);
        }
    }
}
