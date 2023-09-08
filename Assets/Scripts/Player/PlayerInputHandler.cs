using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public Action JumpButtonPushed;
    public Action JumpButtonReleased;

    private bool _isJumpInputBlocked = false;
 
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

        //Debug.Log("main attack ");
    }

    public void ResetJumpInputBlock() => _isJumpInputBlocked = false;
}
