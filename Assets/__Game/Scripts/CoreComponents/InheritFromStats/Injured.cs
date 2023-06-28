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
        SOData.IsInjured = true;
        
    }
    public void InjuredOFF()
    {
        SOData.IsInjured = false;
    }
    public void InjuredONandOFF(bool rotate)
    {
        SOData.IsInjured = rotate;
        if (SOData.IsInjured)
        {
            playerData.LowerEPOnInjury();
            base.IsInjured();
        }
        UpdateConditionUI();
    }

}
