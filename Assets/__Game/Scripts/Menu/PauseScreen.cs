using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    PlayerInput _playerInput;
    bool MenuInput;
    bool SelectInput;
    bool CancelInput;
    [SerializeField] GameObject PauseMenuScreen;
    public  bool isOn = true;
    private bool canToggle = true;
    private bool canCloseMenu;
    public float inputCoolDown = .2f;
   
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    public void OnMenuInput(InputAction.CallbackContext context)
    {
        if (!canToggle)
            return;

         MenuInput = context.ReadValue<float>() >= .5f;
        if(MenuInput && !isOn)
        {
            TogglePauseMenu();
        }
        else if(MenuInput && isOn)
        {
            TogglePauseMenu();
        }
    }
    public void OnSelectInput(InputAction.CallbackContext context)
    {
        SelectInput = context.ReadValue<float>() >= .5f;
        

    }

    public void OnCancelInput(InputAction.CallbackContext context)
    {
        CancelInput = context.ReadValue<float>() >= .5f;

    }

    private void TogglePauseMenu()
    {
        PauseMenuScreen.SetActive(isOn);
        isOn = !isOn;
        canToggle = false;
        Invoke(nameof(ResetToggleCooldown), inputCoolDown);
    }

    private void ResetToggleCooldown()
    {
        canToggle = true;
    }
}
