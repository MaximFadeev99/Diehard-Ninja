using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    private DashDirectionIndicator _dashDirectionIndicator;
    private ParticleSystemRenderer _dashPSR;
    private Color _initialColor;
    private float _initialLinearDrag;
    
    public DashState(Player player, string animationCode) : 
        base(player, animationCode) 
    {
        _dashDirectionIndicator = Player.DashDirectionIndicator;
        _dashPSR = Player.DashParticleSystem.GetComponent<ParticleSystemRenderer>();
        _initialColor = Player.SpriteRenderer.color;
    }

    public override void Enter()
    {
        float sloMoTimeModifier = 0.2f;
        float reducedAlpha = 0.6f;
        float reducedGreen = 0.8f;
        float reducedBlue = 0.8f;

        Time.timeScale = sloMoTimeModifier;
        base.Enter();
        Player.SpriteRenderer.color = new Color
            (_initialColor.r, reducedGreen, reducedBlue, reducedAlpha);
        _initialLinearDrag = Rigidbody.drag;
        _dashDirectionIndicator.gameObject.SetActive(true);
        Rigidbody.excludeLayers = PlayerData.EnemyLayerMask;
        Player.DashParticleSystem.Play();
        Player.ActivateInvincibilty();
    }

    public override State TryChange()
    {
        if (Player.IsDashing)
        {
            return this;
        }
        else 
        {
            return Player.AbilitySuperState.DeflectState;
        }
    }

    public override void UpdatePhysicalMotion()
    {                    
        if (Player.IsDashOnHold)
        {
            RotateDashDirectionIndicator();
            RotateSprite();
        }
        else 
        {
            Time.timeScale = 1f;
            Rigidbody.drag = 10f;
            NextXVelocity = InputHandler.DashDirectionInput.x * 30f;
            NextYVelocity = InputHandler.DashDirectionInput.y * 10f;
            base.UpdatePhysicalMotion();
        }
    }

    public override void Exit()
    {
        _dashDirectionIndicator.gameObject?.SetActive(false);
        Rigidbody.drag = _initialLinearDrag;
        Player.SpriteRenderer.color = _initialColor;
        Rigidbody.excludeLayers = PlayerData.NothingLayerMask;
        Player.DashParticleSystem.Stop();
        Player.DeactivateInvincibility();
    }

    private void RotateDashDirectionIndicator() 
    {
        float RotationAngle = Vector2.SignedAngle
            (Vector2.up, InputHandler.DashDirectionInput);
        _dashDirectionIndicator.transform.rotation = Quaternion.Euler(0f, 0f, RotationAngle);        
    }

    private void RotateSprite() 
    {
        if (InputHandler.DashDirectionInput.x < 0)
        {
            Player.SpriteRenderer.flipX = true;
            _dashPSR.flip = Vector2.right;
        }
        else if (InputHandler.DashDirectionInput.x > 0)
        {
            Player.SpriteRenderer.flipX = false;
            _dashPSR.flip = Vector2.zero;
        }
    }
}
