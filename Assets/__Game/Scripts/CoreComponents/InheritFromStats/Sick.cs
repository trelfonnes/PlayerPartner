using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sick : Stats, ISick
{
    

    protected override void Awake()
    {
        base.Awake();
    }

    public void SickON()
    {
        SOData.Sick = true;
    }
    public void SickOFF()
    {
        SOData.Sick = false;
    }
    public void SickONandOFF(bool rotate)
    {
        SOData.Sick = rotate;
        if (SOData.Sick)
        {
            base.IsSick();
        }
    }

}
