using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTarget : MonoBehaviour, IDartTarget
{
    [SerializeField] GateForSwitches gfs;

    public void BullsEye()
    {
        TargetHit();
    }

    void TargetHit()
    {
        if (gfs)
        {
            gfs.GateOnOff();
        }
    }
}
