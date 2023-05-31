using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : CoreComponent
{//this Stats class hold all events for single responsiblity.
 //it is the script connected to all other corecomponents
 //PlayerData can only be changed via this Stats script.
 //use Interfaces within the inheriting components to change elsewhere.
    public event Action onCurrentHealthZero;

    protected PlayerData playerData;

    protected override void Awake()
    {
        playerData = new PlayerData();

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    protected virtual void CurrentHealthZero()
    {
        onCurrentHealthZero?.Invoke();
    }
   

    #region Interface required functions

    #endregion
}
