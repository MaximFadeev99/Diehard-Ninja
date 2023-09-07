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
 
    public void DidMovement(InputAction.CallbackContext context) 
    {
        MovementInput = context.ReadValue<Vector2>();
        //Debug.Log("movement " + MovementInput);
    }

    public void DidJump(InputAction.CallbackContext context) 
    {
        if (context.started)
            JumpButtonPushed?.Invoke();

        if (context.canceled)
            JumpButtonReleased?.Invoke();

        //Debug.Log("Jumping");
    }

    public void DidMainAttack(InputAction.CallbackContext context) 
    {

        //Debug.Log("main attack ");
    }
}
