using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDeflectable
{
    [SerializeField] private BulletData _bulletData;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private Vector2 _flyDirection;

    Vector2 IDeflectable.CurrentVelocity { get => _flyDirection;}

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.excludeLayers = LayerMask.GetMask("Enemy");
        _spriteRenderer.sprite = _bulletData.Sprite;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _flyDirection.normalized * _bulletData.FlySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
        else if (collision.TryGetComponent(out IDamagable iDamagable)) 
        {
            iDamagable.TakeDamage(_bulletData.Damage);
            Destroy(gameObject);
        }
    }

    public void SetFlyDirection(Vector2 flyDirection) 
    {
        _flyDirection = flyDirection;
    }

    public void ChangeDirection(Vector2 newVector2, int newLayer)
    {
        float newRotationAngle;

        _flyDirection = newVector2;
        newRotationAngle = Mathf.Atan2(_flyDirection.y, _flyDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0,0, newRotationAngle);
        _boxCollider.excludeLayers = LayerMask.GetMask("Nothing");
        gameObject.layer = newLayer;
    }
}
