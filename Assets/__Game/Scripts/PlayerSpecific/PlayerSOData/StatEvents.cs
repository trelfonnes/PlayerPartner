using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "newStatEvents", menuName = "StatEvents Data/StatEvents Data")]

public class StatEvents : ScriptableObject
{
    public event Action onCurrentHealthZero;
    public event Action onCurrentHealthFull;
    public event Action onCurrentEPZero2;
    public event Action onCurrentEPZero3;
    public event Action onInjured;
    public event Action onSick;
    public event Action onCurrentStaminaZero;
    public event Action onCurrentStaminaFull;
    public event Action onCurrentPoiseZero;

     public void CurrentHealthZero()
    {
        if (onCurrentHealthZero != null)
            onCurrentHealthZero?.Invoke();
    }

     public void CurrentHealthFull()
    {
        if (onCurrentHealthFull != null)
            onCurrentHealthFull.Invoke();
    }
     public void CurrentEPZero2()
    {
        if (onCurrentEPZero2 != null)
        {
            Debug.Log("event triggered");
            onCurrentEPZero2.Invoke();
        }
    }
    public void CurrentEPZero3()
    {
        if (onCurrentEPZero3 != null)
        {
            onCurrentEPZero3.Invoke();
        }
    }

     public void IsInjured()
    {
        if (onInjured != null)
        {
            onInjured.Invoke();
        }

    }
    public void IsSick()
    {
        if (onSick != null)
            onSick.Invoke();
    }

     public void CurrentStaminaFull()
    {
        if (onCurrentStaminaFull != null)
            onCurrentStaminaFull.Invoke();
    }
     public void CurrentStaminaZero()
    {
        if (onCurrentStaminaZero != null)
            onCurrentStaminaZero.Invoke();
    }
     public void CurrentPoiseZero()
    {
        if (onCurrentPoiseZero != null)
            onCurrentPoiseZero.Invoke();
    }

}
