using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class SearchArea : MonoBehaviour
{
    private Scumbag _scumbag;
    private Player _player;
    private BoxCollider2D _boxCollider;
    private ContactFilter2D _contactFilter;
    private bool _isRaycastRequired = false;
    private float _offsetModifier = 0.7f;

    public Action<bool> PlayerGotBehind;

    private void Update()
    {
        if (_isRaycastRequired)
            DoRaycast();

        if (_scumbag.IsPlayerInRange) 
            CheckIfFlipRequired();   
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player)) 
            _isRaycastRequired = true;     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player)) 
        {
            _isRaycastRequired = false;
            _scumbag.SetPlayerInRange(false);
        } 
    }

    public void Set(Scumbag scumbag, float searchAreaWidth, float searchAreaHight)
    {
        _scumbag = scumbag;
        _player = _scumbag.Player;
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.isTrigger = true;
        _boxCollider.size = new Vector2(searchAreaWidth, searchAreaHight);
        _offsetModifier = _scumbag.IsFacingRightFirst ? _offsetModifier : -_offsetModifier;
        _boxCollider.offset = new Vector2(_boxCollider.size.x / 2f * _offsetModifier, 0f);
        _contactFilter.NoFilter();
    }

    private void DoRaycast() 
    {
        int playerLayer = 8;
        int wallLayer = 6;
        Vector2 raycastDirection = (Vector2)_player.transform.position - (Vector2)transform.position; 
        int maxHitCount = 15;
        RaycastHit2D[] _hits = new RaycastHit2D[maxHitCount];

        Physics2D.Raycast(transform.position, raycastDirection, _contactFilter, 
            _hits, _boxCollider.size.x);

        for (int i = 0; i < _hits.Length; i++) 
        {
            if (_hits[i].collider.gameObject.layer == wallLayer)
            {
                return;
            }
            else if (_hits[i].collider.gameObject.layer == playerLayer) 
            {
                _isRaycastRequired = false;
                _scumbag.SetPlayerInRange(true);
                return;
            }
        }       
    }

    private void CheckIfFlipRequired() 
    {
        if ((_scumbag.IsFacingRight && _player.transform.position.x < transform.position.x) ||
            (_scumbag.IsFacingRight == false && _player.transform.position.x > transform.position.x))
        {
            _offsetModifier = -_offsetModifier;
            _boxCollider.offset = new Vector2(_boxCollider.size.x / 2f * _offsetModifier, 0f);
            PlayerGotBehind.Invoke(!_scumbag.IsFacingRight);
        }
    }
}