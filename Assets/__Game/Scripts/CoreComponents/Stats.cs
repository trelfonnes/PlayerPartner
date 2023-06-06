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
        if(onCurrentHealthZero != null)
        onCurrentHealthZero?.Invoke();
    }

    protected virtual void CurrentHealthFull()
    {   if(onCurrentHealthFull != null)
        onCurrentHealthFull.Invoke();
    }
    protected virtual void CurrentEPZero()
    {
        if (onCurrentEPZero != null)
            onCurrentEPZero.Invoke();
    }
    protected virtual void IsInjured()
    {
        if(onInjured != null)
        onInjured.Invoke();

    }
    protected virtual void IsSick()
    {
        if (onSick != null)
            onSick.Invoke();
    }

    protected virtual void CurrentStaminaFull()
    {
        if (onCurrentStaminaFull != null)
        onCurrentStaminaFull.Invoke();
    }
    protected virtual void CurrentStaminaZero()
    {
        if(onCurrentStaminaZero != null)
        onCurrentStaminaZero.Invoke();
    }
    protected virtual void CurrentPoiseZero()
    {
        if(onCurrentPoiseZero != null)
        onCurrentPoiseZero.Invoke();
    }
    #endregion


    
}
