using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTarget : MonoBehaviour, IDartTarget
{
   

    public void BullsEye()
    {
        TargetHit();
    }

    void TargetHit()
    {
        //Implement functionality for hitting the target
        Debug.Log("Target was hit");
    }
}
