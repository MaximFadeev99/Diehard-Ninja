using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData _bulletData;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;
    private Vector2 _flyDirection;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        //_bulletData = GetComponent<BulletData>();
        _spriteRenderer.sprite = _bulletData.Sprite;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _flyDirection.normalized * _bulletData.FlySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
           if (collision.gameObject.layer == 6 || collision.gameObject.layer == 8)
              Destroy(gameObject);
    }

    public void SetFlyDirection(Vector2 flyDirection) 
    {
        _flyDirection = flyDirection;
    }
}
