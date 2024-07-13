using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedSwitch : MonoBehaviour
{
    [SerializeField] Sprite switchPressed;
    [SerializeField] Sprite switchUnPressed;
    [SerializeField] OpenDoorConditional doorToOpen;
    [SerializeField] GateForSwitches gateToOpen;
    SpriteRenderer spriteRenderer;
    bool isSwitchPressed;


    // This script takes a type and upon interaction with the "Boulder" can call a method from that type.
    //i.e. doorToOpen. If need to do more than just open a door. Reference the type and callit in checkBehaviorTOHappen.

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = switchUnPressed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boulder"))
        {
            isSwitchPressed = true;
            CheckBehaviorToHappen();
            spriteRenderer.sprite = switchPressed;
            Debug.Log("Open a gate or something");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boulder"))
        {
            isSwitchPressed = false;
            CheckBehaviorToHappen();
            spriteRenderer.sprite = switchUnPressed;
            Debug.Log("Gate is closed");
        }
    }

    private void CheckBehaviorToHappen()
    {
        if (isSwitchPressed)
        {
            if (doorToOpen)
            {
                doorToOpen.OpenDoor();
            }
            if (gateToOpen)
            {
                gateToOpen.GateOnOff();
            }//if(somethingelse is referenced)
            //{Do that function}
        }
        else if (!isSwitchPressed)
        {
            if (doorToOpen)
            {
                doorToOpen.CloseDoor();
            }
            if (gateToOpen)
            {
                gateToOpen.GateOnOff();
            }
        }
    }
}
