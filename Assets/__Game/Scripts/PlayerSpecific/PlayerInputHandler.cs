using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput _playerInput;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool[] AttackInputs { get; private set; }
    public bool MenuInput { get; private set; }
    public bool SwitchPlayerInput { get; private set; }
    //public bool SpecialInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool EvolveInput { get; private set; }
    public bool DashInput { get; private set; }

    Coroutine primaryAttackInputCoroutine;
    Coroutine secondaryAttackInputCoroutine;

    //Variables just for dash's double tap input because new input system is trash
    private bool dashInputDown = false;
    private bool isDoubleTap = false;
    private float doubleTapTimeThreshold = 0.2f;
    private float lastTapTime;
    private bool isResettingDash = false;
    private float resetDelay = .01f;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        

    }
    private void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
    public void OnAttackInput(InputAction.CallbackContext context)//primary
    {
        Debug.Log(AttackInputs[(int)CombatInputs.primary] + "for primary attack input");

        if (context.action.triggered)
        {

            // Start the coroutine to handle the input duration
            primaryAttackInputCoroutine = StartCoroutine(HandleAttackInputDuration());
            AttackInputs[(int)CombatInputs.primary] = true;
        }
        else if (context.action.phase == InputActionPhase.Canceled)
        {
            // Stop the coroutine if the input is released
            if (primaryAttackInputCoroutine != null)
            {
                StopCoroutine(primaryAttackInputCoroutine);
                primaryAttackInputCoroutine = null;
            }
            AttackInputs[(int)CombatInputs.primary] = false;
        }
    }
    private IEnumerator HandleAttackInputDuration()
    {
        yield return new WaitForSeconds(0.25f);
        AttackInputs[(int)CombatInputs.primary] = false;
        AttackInputs[(int)CombatInputs.secondary] = false;
    }
    public void OnSpecialInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Start the coroutine to handle the input duration
            secondaryAttackInputCoroutine = StartCoroutine(HandleAttackInputDuration());
            AttackInputs[(int)CombatInputs.secondary] = true;
        }
        else if (context.canceled)
        {
            // Stop the coroutine if the input is released
            if (secondaryAttackInputCoroutine != null)
            {
                StopCoroutine(secondaryAttackInputCoroutine);
                secondaryAttackInputCoroutine = null;
            }
            AttackInputs[(int)CombatInputs.secondary] = false;
        }
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


        MenuInput = context.ReadValue<float>() >= .5f;      

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
    public void OnEvolveInput(InputAction.CallbackContext context)
    {
       
        if (context.ReadValueAsButton())
        {
            EvolveInput = true;
        }
        else
        {
            EvolveInput = false;

        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            float currentTime = Time.time;

            if (currentTime - lastTapTime <= doubleTapTimeThreshold)
            {
                isDoubleTap = true;
            }
            else
            {
                isDoubleTap = false;
            }

            lastTapTime = currentTime;

            dashInputDown = true;
        }

        if (context.started && isDoubleTap)
        {
            dashInputDown = false;
            DashInput = true;
            isDoubleTap = false;

            if (!isResettingDash)
            {
                StartCoroutine(ResetDashInput());
            }
        }
    }

    private IEnumerator ResetDashInput()
    {
        isResettingDash = true;
        yield return new WaitForSeconds(resetDelay);
        DashInput = false;
        isResettingDash = false;
    }
    

}

public enum CombatInputs
{
    primary,
    secondary
}


