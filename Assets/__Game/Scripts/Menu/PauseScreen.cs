using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
   // PlayerInput _playerInput;
    bool MenuInput;
    bool SelectInput;
    bool CancelInput;
    [SerializeField] GameObject PauseMenuScreen;
    [SerializeField] GameObject ArtifactMenuScreen;
    public  bool isOn = true;
    private bool canToggle = true;
    private bool canCloseMenu;
    public float inputCoolDown = .2f;
   
    private void Awake()
    {
       // _playerInput = GetComponent<PlayerInput>();
    }
    /*public void OnMenuInput(InputAction.CallbackContext context)
    {
        if (!canToggle)
            return;

        if (context.ReadValue<float>() >= 0.5f)
        {
            TogglePauseMenu();
            PauseManager.TogglePause();
        }
    }*/

    /* public void OnSelectInput(InputAction.CallbackContext context)
     {
         SelectInput = context.ReadValue<float>() >= .5f;


     }

     public void OnCancelInput(InputAction.CallbackContext context)
     {
         CancelInput = context.ReadValue<float>() >= .5f;

     }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!canToggle) return;
            else
            {
                TogglePauseMenu();
            
            }
        }
    }
    public void TogglePauseMenu()
    {
        PauseMenuScreen.SetActive(isOn);
        ArtifactMenuScreen.SetActive(false);
        
        isOn = !isOn;
       // canToggle = false;
        //Invoke(nameof(ResetToggleCooldown), inputCoolDown);
        PauseManager.TogglePause();

    }

  //  private void ResetToggleCooldown()
    //{
    //    canToggle = true;
    //}
}
