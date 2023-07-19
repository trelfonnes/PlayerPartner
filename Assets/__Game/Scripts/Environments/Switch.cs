using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    private bool switchOn;
    [SerializeField] OpenDoorConditional doorToOpen;
    // This script takes a type and upon interaction with the "Boulder" can call a method from that type.
    //i.e. doorT

    public void Interact()
    {
        switchOn = !switchOn;
        if (switchOn)
        {
            if (doorToOpen)
            {
                doorToOpen.OpenDoor();
            }
            //if(somethingelse is referenced)
            //{Do that function}
        }
        else if (!switchOn)
        {
            if (doorToOpen)
            {
                doorToOpen.CloseDoor();
            }
        }
    
        Debug.Log("Do Something specific to this object" + switchOn);
    }
}
