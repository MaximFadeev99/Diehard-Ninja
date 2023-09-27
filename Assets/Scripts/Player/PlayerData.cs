using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data / Create PlayerData", order = 51)]
public class PlayerData : ScriptableObject
{
    #region Basic Stats
    [Header("Basic Stats")]
    [SerializeField] private float _basicDamage;
    [SerializeField] private int _startHeartCount;

    public float BasicDamage => _basicDamage;
    public int StartHeartCount => _startHeartCount;
    #endregion

    #region Basic Movements
    [Header("Basic Movements")]
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _wallSlideSpeed;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _coyoteJumpTime;

    public float RunSpeed => _runSpeed;
    public float WallSlideSpeed => _wallSlideSpeed;
    public float JumpVelocity => _jumpVelocity;
    public float JumpDuration => _jumpDuration;
    public float CoyoteJumpTime => _coyoteJumpTime;
    #endregion

    #region Ability Related
    [Header("Ability Related")]
    [SerializeField] private float _deflectionCoolDownTime;
    [SerializeField] private float _dashCoolDownTime;
    [SerializeField] private float _dashHoldTime;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashXVelocity;
    [SerializeField] private float _dashYVelocity;

    public float DeflectionCoolDownTime => _deflectionCoolDownTime;
    public float DashCoolDownTime => _dashCoolDownTime;
    public float DashHoldTime => _dashHoldTime;
    public float DashDuration => _dashDuration;
    public float DashXVelocity => _dashXVelocity;
    public float DashYVelocity => _dashYVelocity;
    #endregion

    #region Surrounding Check Related
    [Header("Surrounding Check Related")]
    [SerializeField] private float _groundDistanceCheck;
    [SerializeField] protected float _wallDistanceCheck;

    public float GroundDistanceCheck => _groundDistanceCheck;
    public float WallDistanceCheck => _wallDistanceCheck;
    #endregion

    #region Sounds
    [Header("Sound Related")]
    [SerializeField] private AudioClip _runinngSound;
    [SerializeField] private AudioClip _jumpingSound;
    [SerializeField] private AudioClip _swordAttackSound;

    public AudioClip RuninngSound => _runinngSound;
    public AudioClip JumpingSound => _jumpingSound;
    public AudioClip SwordAttackSound => _swordAttackSound;
    #endregion

    #region Layer Masks
    [Header("Layer Masks")]
    [SerializeField] private LayerMask _wallLayerMask;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private LayerMask _nothingLayerMask;

    public LayerMask WallLayerMask => _wallLayerMask;
    public LayerMask EnemyLayerMask => _enemyLayerMask;
    public LayerMask NothingLayerMask => _nothingLayerMask;
    #endregion
}

