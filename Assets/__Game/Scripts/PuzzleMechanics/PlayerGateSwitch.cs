using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGateSwitch : MonoBehaviour, IInteractable, IBombable
{
    [SerializeField] GateForSwitches gfs;
    public void Explode()
    {
        ChangeGateState();
        //play click sound
    }
    public void Interact()
    {
        ChangeGateState();
        //play click sound
    }
    void ChangeGateState()
    {
        gfs.GateOnOff();
    }
   
}
