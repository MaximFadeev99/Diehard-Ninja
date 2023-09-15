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
    private ContactFilter2D _contactFilter;

    public void LookForPlayer()
    {
        if (_isRaycastRequired)
            DoRaycast();

        //if (_player != null) 
        //{
        //    CheckAlertArea();
        //}   
    }

    public void Set(Humanoid humanoid, float searchAreaWidth, float searchAreaHight) 
    {
        float offsetModifier = 0.7f;

        _humanoid = humanoid;
        BoxCollider2D = GetComponent<BoxCollider2D>();
        BoxCollider2D.isTrigger = true;
        BoxCollider2D.size = new Vector2(searchAreaWidth, searchAreaHight);
        BoxCollider2D.offset = new Vector2(BoxCollider2D.size.x / 2f * offsetModifier, 0f);
        //_alertArea = alertArea;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _player))        
            _isRaycastRequired = true;        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _player)) 
        {
            _isRaycastRequired = false;
            _player = null;
            _humanoid.SetPlayer(_player);
        } 
    }

    private void DoRaycast() 
    {
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
                _humanoid.SetPlayer(_player);
                print("Player is found and set");
                return;
            }
        }       
    }

    //private void CheckAlertArea() 
    //{
    //    Vector2 playerPosition = _player.transform.position;
    //    Vector2 searchAreaCenter = transform.position;

    //    if (Vector2.Distance(playerPosition, searchAreaCenter) < _alertArea
    //        && _humanoid.Player == null)
    //    {
    //        _humanoid.SetPlayer(_player);
    //        //print("Player in Alert Area");
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    float gizmoDrawModifier = 0.85f;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, new Vector3
    //        (BoxCollider2D.size.x * gizmoDrawModifier, BoxCollider2D.size.y * gizmoDrawModifier, 0f));
    //    Gizmos.DrawWireCube(transform.position, new Vector3
    //        (_alertArea, _alertArea, 0f));
    //}
}
