using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using PixelCrushers.DialogueSystem;

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
    private bool muteInput = false;
    private float doubleTapTimeThreshold = 0.2f;
    private float lastTapTime;
    private bool isResettingDash = false;
    private float resetDelay = .01f;
    public bool HasMoveInput => RawMovementInput != Vector2.zero;

    private void OnEnable()
    {
        DialogueManager.Instance.conversationStarted += MuteInputForDialogue;
        DialogueManager.Instance.conversationEnded += ReturnInputFromDialogue;

    }
    
    private void Awake()
    {
        PauseManager.OnPauseStateChanged += HandlePausedStateChanged;
        CutsceneManager.OnCutscenePlaying += HandlePausedStateChanged;
        _playerInput = GetComponent<PlayerInput>();
       

    }
    void MuteInputForDialogue(Transform actor)
    {
        _playerInput.DeactivateInput();
    }
    void ReturnInputFromDialogue(Transform actor)
    {
        _playerInput.ActivateInput();
    }
    void HandlePausedStateChanged(bool isPaused)
    {
        if (isPaused)
        {
            muteInput = true;
        }
        else
        {
            muteInput = false;
        }
    }
    private void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (muteInput)
        {
            Debug.Log(muteInput);
            NormInputX = 0;
            NormInputY = 0;
            return;
        }
        RawMovementInput = context.ReadValue<Vector2>();
            NormInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormInputY = Mathf.RoundToInt(RawMovementInput.y);
        
    }
    public void OnAttackInput(InputAction.CallbackContext context)//primary
    {
        if (muteInput)
            return;
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
        if (muteInput)
            return; 
        
    
        if (context.action.triggered)
            {
                // Start the coroutine to handle the input duration
                //     secondaryAttackInputCoroutine = StartCoroutine(HandleAttackInputDuration());
                AttackInputs[(int)CombatInputs.secondary] = true;
            }
            else if (context.action.phase == InputActionPhase.Canceled)
            {
                // Stop the coroutine if the input is released
                //  if (secondaryAttackInputCoroutine != null)
                //   {
                //      StopCoroutine(secondaryAttackInputCoroutine);
                //     secondaryAttackInputCoroutine = null;
                //  }
                AttackInputs[(int)CombatInputs.secondary] = false;
            }
        
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (muteInput)
            return;
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
        if (muteInput)
            return;

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
        if (muteInput)
            return;
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
        if (muteInput)
            return;
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
    public void ChangeMuteInput(bool muteInput)
    {
        this.muteInput = muteInput;
    }

    private void OnDisable()
    {
        PauseManager.OnPauseStateChanged -= HandlePausedStateChanged;
        CutsceneManager.OnCutscenePlaying -= HandlePausedStateChanged;
  

    }
    private void OnApplicationQuit()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.conversationStarted -= MuteInputForDialogue;
            DialogueManager.Instance.conversationEnded -= ReturnInputFromDialogue;
        }
    }
}

public enum CombatInputs
{
    primary,
    secondary
}


