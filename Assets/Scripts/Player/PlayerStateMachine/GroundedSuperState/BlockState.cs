using System.Collections.Generic;
using UnityEngine;

public class BlockState : PlayerState
{
    private ContactFilter2D _blockFilter = new();

    public BlockState(PlayerStateMachine playerStateMachine, int animationCode) : 
        base(playerStateMachine, animationCode) 
    {
        _blockFilter.SetLayerMask(PlayerData.EnemyLayerMask);
        _blockFilter.useTriggers = true;
    }

    public override void Enter()
    {
        SpriteRenderer.flipX = Player.IsFacingRight;
        base.Enter();
    }

    public override State TryChange()
    {
        if (Player.IsBlocking)
        {
            return this;
        }
        else if (Player.IsAttacking) 
        {
            return PlayerStateMachine.GroundedSuperState.AttackState;
        }
        else
        {
            return PlayerStateMachine.GroundedSuperState.IdleState;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        List<Collider2D> results = new();
        Vector2 blockAreaSize = new(0.1f, 1.3f);
        float blockAreaXOffset = Player.IsFacingRight ? 0.35f : -0.35f;
        Physics2D.OverlapBox(new Vector2(Player.transform.position.x + blockAreaXOffset, Player.transform.position.y),
            blockAreaSize, 0f, _blockFilter, results);

        foreach (Collider2D result in results)
        {
            if (result.TryGetComponent(out IDeflectable iDeflectable)) 
            {
                float yDirection = Random.Range(-0.9f, 0.9f);
                float xDirection = iDeflectable.CurrentVelocity.x * -1f;
                Vector2 newDirection = new(xDirection, yDirection);
                
                iDeflectable.ChangeDirection(newDirection, Player.gameObject.layer);
            }
        }
    }

    public override void Exit() => SpriteRenderer.flipX = !Player.IsFacingRight;
}