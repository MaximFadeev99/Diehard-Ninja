using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private AudioClip _runinngSound;
    [SerializeField] private AudioClip _jumpingSound;
    [SerializeField] private AudioClip _swordAttackSound;

    public AudioClip RuninngSound => _runinngSound;
    public AudioClip JumpingSound => _jumpingSound;
    public AudioClip SwordAttackSound => _swordAttackSound;
    public float RunSpeed { get; private set; } = 5f;
    public float WallSlideSpeed { get; private set; } = 1f;
    public float GroundDistanceCheck { get; private set; } = 0.7f;
    public float WallDistanceCheck { get; private set; } = 0.6f;
    public float JumpVelocity { get; private set; } = 3f;
    public float JumpDuration { get; private set; } = 0.5f;
    public float CoyoteJumpTime { get; private set; } = 0.2f;
    public float DeflectionCoolDownTime { get; private set; } = 0.1f;
    public float DashCoolDownTime { get; private set; } = 3f;
    public float DashHoldTime { get; private set; } = 0.4f;
    public float DashDuration { get; private set; } = 0.2f;
    public float DashXVelocity { get; private set; } = 30f;
    public float DashYVelocity { get; private set; } = 30f;
    public float BasicDamage { get; private set; } = 1f;
    public LayerMask WallLayerMask { get; private set; } 
    public LayerMask EnemyLayerMask { get; private set; }
    public LayerMask NothingLayerMask { get; private set; }

    private void Awake()
    {
        WallLayerMask = LayerMask.GetMask("Walls");
        EnemyLayerMask = LayerMask.GetMask("Enemy");
        NothingLayerMask = LayerMask.GetMask("Nothing");
    }
}
