using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    private Timer _dealDamageTimer = new();
    private Timer _attackTimer;
    private List<int> _attacks = new();
    private ContactFilter2D _attackFilter = new();
    private int _attack1 = AnimationData.Attack1;
    private int _attack2 = AnimationData.Attack2;
    private int _attack3 = AnimationData.Attack3;
    private int _nextAttack;
    private int _currentAttackCount;
    private bool _isTimeForAttack1 = true;
    private float _attackResetTime = 1.5f;

    public AttackState(PlayerStateMachine playerStateMachine, int animationCode) 
        : base(playerStateMachine, animationCode) 
    {
        _attackTimer = Player.AttackTimer;
        _attacks.Add(_attack1);
        _attacks.Add(_attack2);
        _attacks.Add(_attack3);
        _attackFilter.SetLayerMask(PlayerData.EnemyLayerMask);
        AudioClip = PlayerData.SwordAttackSound;
    }

    public override void Enter()
    {
        float dealDamageDelay = 0.2f;

        SetNextAttack();
        _dealDamageTimer.TimeIsUp += DealDamage;
        _dealDamageTimer.Start(dealDamageDelay);
        Player.Animator.Play(_nextAttack);
        PlaySound(1f, false);
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

    public override void Exit() => _dealDamageTimer.TimeIsUp -= DealDamage;

    public void ResetAttackTimer() => _isTimeForAttack1 = true;

    private void SetNextAttack() 
    {        
        if (_currentAttackCount == 0 || _isTimeForAttack1)
        {
            RestartAttackSequence();
        }
        else
        {
            if (_currentAttackCount <= _attacks.Count - 1)
            {
                _nextAttack = _attacks[_currentAttackCount];
                _currentAttackCount++;
                _attackTimer.Start(_attackResetTime);
            }
            else
            {
                RestartAttackSequence();
            }
        }
    }

    private void RestartAttackSequence() 
    {
        _currentAttackCount = 0;
        _nextAttack = _attacks[_currentAttackCount];
        _currentAttackCount++;
        _isTimeForAttack1 = false;
        _attackTimer.Start(_attackResetTime);
    }

    private void DealDamage() 
    {
        List<Collider2D> results = new();              
        Vector2 attackAreaSize = new (1f, 1.3f);
        float attackAreaXOffset = Player.IsFacingRight ? 0.5f : -0.5f;

        Physics2D.OverlapBox(new Vector2(Player.transform.position.x + attackAreaXOffset, Player.transform.position.y),
            attackAreaSize, 0f, _attackFilter, results);

        foreach (Collider2D result in results)
        {
            if (result.TryGetComponent(out IDamagable iDamagable))
                iDamagable.TakeDamage(Player.PlayerData.BasicDamage);                    
        }
    }
}