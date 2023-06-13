using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    PlayerInput _playerInput;
    bool MenuInput;
    [SerializeField] GameObject PauseMenuScreen;
    public  bool isOn = false;
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

        bool MenuInput = context.ReadValue<float>() >= .5f;
        if(MenuInput && !isOn)
        {
            TogglePauseMenu();
        }
        else if(MenuInput && isOn)
        {
            TogglePauseMenu();
        }
    }
    // Update is called once per frame
    void Update()
    {
       


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
