using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Data / Create BulletData", order = 51)]
public class BulletData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private LayerMask _nothingLayerMask;

    public Sprite Sprite => _sprite;
    public float FlySpeed => _flySpeed;
    public float Damage => _damage;
    public LayerMask NothingLayerMask => _nothingLayerMask;
    public LayerMask EnemyLayerMask => _enemyLayerMask;
}