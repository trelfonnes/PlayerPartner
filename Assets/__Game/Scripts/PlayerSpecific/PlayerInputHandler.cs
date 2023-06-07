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
    public bool[] AttackInputs { get; private set; }
    public bool MenuInput { get; private set; }
    public bool SwitchPlayerInput { get; private set; }
    public bool SpecialInput { get; private set; }
    public bool InteractInput { get; private set; }
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    //write function to assign to the unity events found in the playerinput on Player GO

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
       // TODO: example of how to get a more custom and accurate input reading than the three events
       // _playerInput.actions["Attack"].ReadValue<float>() > 0
        if (context.started)
        {
            // TODO add attackinputs
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
        if (context.ReadValueAsButton())
        {
            InteractInput = true;

        }
        else
        {
            InteractInput = false;

        }
    }
    public void OnMenuInput(InputAction.CallbackContext context)
    {

    }
    public void OnPlayerSwitchInput(InputAction.CallbackContext context)
    {
       if (context.ReadValueAsButton())
        {
                SwitchPlayerInput = true;
        }
        else
        {
            SwitchPlayerInput = false;
        }

            

    }
}
