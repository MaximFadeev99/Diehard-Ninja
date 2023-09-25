using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullets / Create new bullet", order = 51)]
public class BulletData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _damage;

    public Sprite Sprite => _sprite;
    public float FlySpeed => _flySpeed;
    public float Damage => _damage;
}
