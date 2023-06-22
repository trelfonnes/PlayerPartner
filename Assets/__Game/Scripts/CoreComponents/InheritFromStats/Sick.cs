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
        SOData.IsSick = true;
    }
    public void SickOFF()
    {
        SOData.IsSick = false;
    }
    public void SickONandOFF(bool rotate)
    {
        SOData.IsSick = rotate;
        if (SOData.IsSick)
        {
            base.IsSick();
        }
    }

}
