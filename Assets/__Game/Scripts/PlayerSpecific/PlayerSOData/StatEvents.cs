using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "newStatEvents", menuName = "StatEvents Data/StatEvents Data")]

public class StatEvents : ScriptableObject
{
    public event Action onCurrentHealthZero;
    public event Action onCurrentHealthFull;
    public event Action onCurrentEPZero;
    public event Action onInjured;
    public event Action onSick;
    public event Action onCurrentStaminaZero;
    public event Action onCurrentStaminaFull;
    public event Action onCurrentPoiseZero;
    public event Action onPartnerFullyRestored;
   
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
     public void CurrentEPZero()
    {
        if (onCurrentEPZero != null)
        {
            onCurrentEPZero.Invoke();
        }
    }
    public void PartnerRestored()
    {
        if (onPartnerFullyRestored != null)
        {
            onPartnerFullyRestored.Invoke();
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
