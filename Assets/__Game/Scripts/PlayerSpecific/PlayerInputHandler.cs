using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput _playerInput;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    //write function to assign to the unity events found in the playerinput on Player GO

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        Debug.Log(RawMovementInput);
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Attack pushed down now");

        }
        if (context.performed)
        {
            Debug.Log("Attack is held down");
        }
        if(context.canceled)
        {
            Debug.Log("Attack is released");
        }
    }
    public void OnSpecialInput(InputAction.CallbackContext context)
    {

    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {

    }
    public void OnMenuInput(InputAction.CallbackContext context)
    {

    }
    public void OnPlayerSwitchInput(InputAction.CallbackContext context)
    {

    }
}
