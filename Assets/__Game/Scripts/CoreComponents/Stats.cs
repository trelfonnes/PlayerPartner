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
    public event Action onCurrentHealthFull;
    public event Action onCurrentEPZero;
    public event Action onInjured;
    public event Action onSick;
    public event Action onCurrentStaminaZero;
    public event Action onCurrentStaminaFull;
    public event Action onCurrentPoiseZero;

    protected PlayerData playerData;

   

    protected override void Awake()
    {
        playerData = new PlayerData();

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    #region Events for stat changes
    protected virtual void CurrentHealthZero()
    {
        onCurrentHealthZero?.Invoke();
    }

    protected virtual void CurrentHealthFull()
    {
        onCurrentHealthFull.Invoke();
    }
    protected virtual void CurrentEPZero()
    {
        onCurrentEPZero.Invoke();
    }
    protected virtual void IsInjured()
    {
        onInjured.Invoke();

    }
    protected virtual void IsSick()
    {
        onSick.Invoke();
    }

    protected virtual void CurrentStaminaFull()
    {
        onCurrentStaminaFull.Invoke();
    }
    protected virtual void CurrentStaminaZero()
    {
        onCurrentStaminaZero.Invoke();
    }
    protected virtual void CurrentPoiseZero()
    {
        onCurrentPoiseZero.Invoke();
    }
    #endregion


    #region Interface required functions

    #endregion
}
