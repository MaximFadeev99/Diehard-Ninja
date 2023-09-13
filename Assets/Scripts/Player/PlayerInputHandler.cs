using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Player _player;
    private Camera _camera;
    public Vector2 MovementInput { get; private set; }
    public Vector2 DashDirectionInput { get; private set; }
    public Action JumpButtonPushed;
    public Action JumpButtonReleased;
    public Action MainAttackButtonPushed;
    public Action DeflectionButtonPushed;
    public Action DashButtonPushed;
    public Action DashButtonReleased;

    public bool IsBlocking { get; private set; }

    private bool _isJumpInputBlocked = false;
    private bool _isAttackInputBlocked = false;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _camera = Camera.main;
    }

    public void DidMovement(InputAction.CallbackContext context) 
    {
        MovementInput = context.ReadValue<Vector2>();
        //Debug.Log("movement " + MovementInput);
    }

    public void DidJump(InputAction.CallbackContext context) 
    {
        if (context.started && _isJumpInputBlocked == false) 
        {
            JumpButtonPushed?.Invoke();
            _isJumpInputBlocked = true;
        }
            

        if (context.canceled)
            JumpButtonReleased?.Invoke();

        //Debug.Log("Jumping");
    }

    public void DidMainAttack(InputAction.CallbackContext context) 
    {
        if (context.started) //&& _isAttackInputBlocked == false) 
        {
            MainAttackButtonPushed?.Invoke();
            //_isAttackInputBlocked = true;
            //Debug.Log("Main Attack");
        }
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
        {
            DeflectionButtonPushed?.Invoke();
            //Debug.Log("Did deflection");
        }
    }

    public void DidDash(InputAction.CallbackContext context) 
    {
        if (context.started) 
        {
            DashButtonPushed?.Invoke();
        }

        if (context.canceled) 
        {
            DashButtonReleased?.Invoke();
        }
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
    public void ResetAttackInputBlock() => _isAttackInputBlocked = false;
}
