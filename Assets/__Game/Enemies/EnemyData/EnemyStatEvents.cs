using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "newEnemyStatEvents", menuName = "EnemyStatEvents Data/EnemyStatEvents Data")]

public class EnemyStatEvents : ScriptableObject
{
    public event Action onHealthLow;
    public event Action onHealthZero;
    public event Action onPoiseZero;
    public event Action onPoiseRefilled;
    public event Action onBattleStart;
    public event Action onStaminaZero;
    public void BattleStart()
    {
        if (onBattleStart != null)
        {
            onBattleStart?.Invoke();
        }
    }
    public void HealthZero()
    {
        if(onHealthZero != null)
        {
            onHealthZero.Invoke();
        }
    }
    public void HealthLow()
    {
        if(onHealthLow != null)
        {
            onHealthLow.Invoke();
        }
    }

    public void PoiseZero()
    {
        if(onPoiseZero != null)
        {
            onPoiseZero.Invoke();
        }
    }
    public void PoiseRefilled()
    {
        if(onPoiseRefilled != null)
        {
            onPoiseRefilled.Invoke();
        }
    }
    public void StaminaZero()
    {
        if(onStaminaZero != null)
        {
            onStaminaZero.Invoke();
        }
    }

}
