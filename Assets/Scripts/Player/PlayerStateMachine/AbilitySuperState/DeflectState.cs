using System.Collections.Generic;
using UnityEngine;

public class DeflectState : PlayerState
{
    private List<Collider2D> _results = new();
    private ContactFilter2D _attackFilter = new();
    
    public DeflectState(PlayerStateMachine playerStateMachine,int animationCode) 
        : base(playerStateMachine, animationCode) 
    {
        _attackFilter.SetLayerMask(PlayerData.EnemyLayerMask);
        _attackFilter.useTriggers = true;
    }

    public override void Enter()
    {
        base.Enter();
        Player.ActivateInvincibilty();        
    }

    public override State TryChange()
    {
        if (Player.IsDeflecting)
        {
            return this;
        }
        else if (Player.IsDashing)
        {
            return PlayerStateMachine.AbilitySuperState.DashState;
        }
        else 
        {
            return this;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        UpdateVelocity();
        DealDamageAndDeflect();
    }

    public override void Exit()
    {
        Player.DeactivateInvincibility();
        _results.Clear();
    }

    private void UpdateVelocity() 
    {
        float xVelocityModifier = 7f;
        float xVelocity;

        if (InputHandler.MovementInput.x != 0)
        {
            xVelocity = Player.IsFacingRight ? xVelocityModifier : xVelocityModifier * -1;
        }
        else
        {
            xVelocity = 0f;
        }

        NextXVelocity = xVelocity;
        NextYVelocity = Rigidbody.velocity.y;
        base.UpdatePhysicalMotion();
    }

    private void DealDamageAndDeflect() 
    {
        float attackAreaRadius = 1f;
        float damageModifier = 3f;
        float increasedDamage = PlayerData.BasicDamage * damageModifier;
        List<Collider2D> currentAttackResults = new();

        Physics2D.OverlapCircle(Player.transform.position, attackAreaRadius, _attackFilter, currentAttackResults);

        foreach (Collider2D result in currentAttackResults) 
        {
            if (result.gameObject.TryGetComponent(out IDamagable iDamagable) && _results.Contains(result) == false)
            {
                iDamagable.TakeDamage(increasedDamage);
                _results.Add(result);
            }            
            else if (result.gameObject.TryGetComponent(out IDeflectable iDeflectable)) 
            {
                iDeflectable.ChangeDirection(-iDeflectable.CurrentVelocity, Player.gameObject.layer);
            }
        }
    }
}