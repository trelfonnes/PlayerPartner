using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetDialogueUIButton : MonoBehaviour
{
    [SerializeField] GameObject initialUIButton;

    private void OnEnable()
    {
        SetInitialMenuButton();
    }
    void SetInitialMenuButton() //Need to set the selected button manually when Menu opens because event system can't track it itself.
    {
        if (initialUIButton != null)
        {
            EventSystem.current.firstSelectedGameObject = initialUIButton;
            EventSystem.current.SetSelectedGameObject(initialUIButton);
        }
    }
}
