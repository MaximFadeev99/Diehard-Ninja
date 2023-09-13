using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private DashDirectionIndicator _dashDirectionIndicator;
    private ParticleSystemRenderer _dashPSR;
    private Color _initialColor;
    private float _initialLinearDrag;
    private float _finalDashX;
    private float _finalDashY;

    public DashState(PlayerStateMachine playerStateMachine, string animationCode) : 
        base(playerStateMachine, animationCode) 
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
            return PlayerStateMachine.AbilitySuperState.DeflectState;
        }
    }

    public override void UpdatePhysicalMotion()
    {
        float increasedDrag = 10f;
        
        if (Player.IsDashOnHold)
        {
            RotateDashDirectionIndicator();
            RotateSprite();
            _finalDashX = InputHandler.DashDirectionInput.x;
            _finalDashY = InputHandler.DashDirectionInput.y;
        }
        else 
        {
            Time.timeScale = 1f;
            Rigidbody.drag = increasedDrag;
            NextXVelocity = _finalDashX * PlayerData.DashXVelocity;
            NextYVelocity = _finalDashY * PlayerData.DashYVelocity;
            base.UpdatePhysicalMotion();
        }
    }

    public void ForceExit()
    {
        float dashExitYVelocity = -0.01f;
        
        _dashDirectionIndicator.gameObject?.SetActive(false);
        Rigidbody.drag = _initialLinearDrag;
        Player.SpriteRenderer.color = _initialColor;
        Rigidbody.excludeLayers = PlayerData.NothingLayerMask;
        Player.DashParticleSystem.Stop();
        Player.DeactivateInvincibility();
        Rigidbody.velocity = new Vector2 (0f, dashExitYVelocity);
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
