using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemTriggerManager : MonoBehaviour
{
    [SerializeField] EventTriggerAbstractClass objectToTrigger;
    public void TriggerKeyItemEvent()
    {
        objectToTrigger.TriggerEvent();
    }
}
