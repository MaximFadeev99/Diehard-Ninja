using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]

public class PlayerInputHandler : MonoBehaviour
{   
    public Action JumpButtonPushed;
    public Action JumpButtonReleased;
    public Action MainAttackButtonPushed;
    public Action DeflectionButtonPushed;
    public Action DashButtonPushed;
    public Action DashButtonReleased;

    private Player _player;
    private Camera _camera;
    private bool _isJumpInputBlocked = false;

    public bool IsBlocking { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 DashDirectionInput { get; private set; }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _camera = Camera.main;
    }

    public void DidMovement(InputAction.CallbackContext context) => 
        MovementInput = context.ReadValue<Vector2>();

    public void DidJump(InputAction.CallbackContext context) 
    {
        if (context.started && _isJumpInputBlocked == false) 
        {
            JumpButtonPushed?.Invoke();
            _isJumpInputBlocked = true;
        }            

        if (context.canceled)
            JumpButtonReleased?.Invoke();
    }

    public void DidMainAttack(InputAction.CallbackContext context) 
    {
        if (context.started)
            MainAttackButtonPushed?.Invoke();
    }

    public void DidBlock(InputAction.CallbackContext context) 
    {
        if (context.started) 
            IsBlocking = true;

        if (context.canceled) 
            IsBlocking = false;           
    }

    public void DidDeflection(InputAction.CallbackContext context) 
    {
        if (context.started) 
            DeflectionButtonPushed?.Invoke();
    }

    public void DidDash(InputAction.CallbackContext context) 
    {
        if (context.started) 
            DashButtonPushed?.Invoke();

        if (context.canceled) 
            DashButtonReleased?.Invoke();
    }

    public void SetDashDirection(InputAction.CallbackContext context) 
    {              
        if (_player.IsDashing) 
        {
            Vector2 rawDashInput = context.ReadValue<Vector2>();
            DashDirectionInput = _camera.ScreenToWorldPoint(rawDashInput) - _player.transform.position;
            DashDirectionInput = DashDirectionInput.normalized;
        }
    }

    public void ResetJumpInputBlock() => _isJumpInputBlocked = false;
}