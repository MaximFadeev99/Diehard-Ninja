using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private string _attack1 = "Attack1";
    private string _attack2 = "Attack2";
    private string _attack3 = "Attack3";
    private List<string> _attacks = new List<string>();
    private string _nextAttack;
    private int _currentAttackCount = 0;
    private bool _isTimeUp = false;

    public AttackState(Player player, string animationCode) 
        : base(player, animationCode) 
    {
        _attacks.Add(_attack1);
        _attacks.Add(_attack2);
        _attacks.Add(_attack3);
    }

    public override void Enter()
    {
        SetNextAttack();
        Player.Animator.Play(_nextAttack);
    }

    public override State TryChange()
    {
        if (Player.IsBlocking)
        {
            return Player.GroundedSuperState.BlockState;
        }
        else if (Player.IsAttacking)
        {
            return this;
        }
        //else if (Player.IsJumping)
        //{
        //    return Player.GroundedSuperState.JumpState;
        //}
        //else if (InputHandler.MovementInput.x != 0 && Player.IsJumping == false)
        //{
        //    return Player.GroundedSuperState.RunState;
        //}
        else
        {
            return Player.GroundedSuperState.IdleState;
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

    private void SetNextAttack() 
    {
        if (_currentAttackCount == 0 || _isTimeUp)
        {
            _currentAttackCount = 0;
            _nextAttack = _attacks[_currentAttackCount];
            _currentAttackCount++;
            _isTimeUp = false;
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

    public void ResetTimer() => _isTimeUp = true;
}
