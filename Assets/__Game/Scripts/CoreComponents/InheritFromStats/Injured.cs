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
        playerData.Injured = true;
    }
    public void InjuredOFF()
    {
        playerData.Injured = false;
    }
    public void InjuredONandOFF(bool rotate)
    {
        playerData.Injured = rotate;
        if (playerData.Injured)
        {
            base.IsInjured();
        }
    }

}
