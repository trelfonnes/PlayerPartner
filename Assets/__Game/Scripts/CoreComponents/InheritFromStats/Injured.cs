using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injured : Stats, IInjured
{
   


    protected override void Awake()
    {
        base.Awake();
    }

    public void InjuredON()
    {
        SOData.Injured = true;
    }
    public void InjuredOFF()
    {
        SOData.Injured = false;
    }
    public void InjuredONandOFF(bool rotate)
    {
        SOData.Injured = rotate;
        if (SOData.Injured)
        {
            base.IsInjured();
        }
    }

}
