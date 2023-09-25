using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    private string _attack1 = "Attack1";
    private string _attack2 = "Attack2";
    private string _attack3 = "Attack3";
    private List<string> _attacks = new();
    private string _nextAttack;
    private int _currentAttackCount = 0;
    private bool _isTimeForAttack1 = false;
    private ContactFilter2D _attackFilter = new();
    private Timer _dealDamageTimer = new();
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    public AttackState(PlayerStateMachine playerStateMachine, string animationCode) 
        : base(playerStateMachine, animationCode) 
    {
        _attacks.Add(_attack1);
        _attacks.Add(_attack2);
        _attacks.Add(_attack3);
        _attackFilter.SetLayerMask(PlayerData.EnemyLayerMask);
        _audioSource = Player.AudioSource;
        _audioClip = Player.PlayerData.SwordAttackSound;

    }

    public override void Enter()
    {
        float dealDamageDelay = 0.2f;

        SetNextAttack();
        _dealDamageTimer.TimeIsUp += DealDamage;
        _dealDamageTimer.Start(dealDamageDelay);
        Player.Animator.Play(_nextAttack);
        _audioSource.clip = _audioClip;
        _audioSource.volume = 1f;
        _audioSource.loop = false;
        _audioSource.Play();
    }

    public override State TryChange()
    {              
        if (_dealDamageTimer.IsActive)
            _dealDamageTimer.Tick();
        
        if (Player.IsBlocking)
        {
            return PlayerStateMachine.GroundedSuperState.BlockState;
        }
        else if (Player.IsAttacking)
        {
            return this;
        }
        else
        {
            return PlayerStateMachine.GroundedSuperState.IdleState;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        float dashDirection;
        float dashVelocity = 35f;

        if (Player.IsUsingAttackDash == false)
        {
            NextXVelocity = Rigidbody.velocity.x;
            NextYVelocity = Rigidbody.velocity.y;
            base.UpdatePhysicalMotion();
        }
        else
        {
            if (_currentAttackCount == 1 || _currentAttackCount == 2)
            {
                dashDirection = Player.IsFacingRight ? 1 : -1;
                Rigidbody.AddForce(new Vector2(dashDirection * dashVelocity, 0), ForceMode2D.Impulse);
            }

            Player.IsUsingAttackDash = false;
        }
    }

    public override void Exit()
    {
        _dealDamageTimer.TimeIsUp -= DealDamage;
    }

    private void SetNextAttack() 
    {
        if (_currentAttackCount == 0 || _isTimeForAttack1)
        {
            _currentAttackCount = 0;
            _nextAttack = _attacks[_currentAttackCount];
            _currentAttackCount++;
            _isTimeForAttack1 = false;
        }
        else
        {
            if (_currentAttackCount <= _attacks.Count - 1)
            {
                _nextAttack = _attacks[_currentAttackCount];
                _currentAttackCount++;
            }
            else
            {
                _currentAttackCount = 0;
                _nextAttack = _attacks[_currentAttackCount];
                _currentAttackCount++;
            }
        }
    }

    private void DealDamage() 
    {
        List<Collider2D> results = new();              
        Vector2 attackAreaSize = new Vector2(1f, 1.3f);
        float attackAreaXOffset = Player.IsFacingRight ? 0.5f : -0.5f;

        Physics2D.OverlapBox(new Vector2(Player.transform.position.x + attackAreaXOffset, Player.transform.position.y),
            attackAreaSize, 0f, _attackFilter, results);

        foreach (Collider2D result in results)
        {
            if (result.TryGetComponent(out IDamagable iDamagable))
            {
                iDamagable.TakeDamage(Player.PlayerData.BasicDamage);
            }                     
        }
    }
    public void ResetAttackTimer() => _isTimeForAttack1 = true;
}
