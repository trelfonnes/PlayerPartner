using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    private bool switchOn;
   public void Interact()
    {
        switchOn = !switchOn;
        
        Debug.Log("Do Something specific to this object" + switchOn);
    }
}
