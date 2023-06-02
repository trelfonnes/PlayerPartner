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
        playerData.Sick = true;
    }
    public void SickOFF()
    {
        playerData.Sick = false;
    }
    public void SickONandOFF(bool rotate)
    {
        playerData.Sick = rotate;
        if (playerData.Sick)
        {
            base.IsSick();
        }
    }

}
