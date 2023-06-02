using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : Stats, IHealthChange //interfaces for decreasing health and increasing
{//CANNOT be a child object of Stats. Has to be a child object of coreHandler or else error is thrown.
    //hold events in functions within stats and call them where needed in respective scripts 
    protected override void Awake()
    {
        base.Awake();
      //  playerData.CurrentHealth = playerData.MaxHealth;

    }
    public void DecreaseHealth(float amount)
    {
        playerData.CurrentHealth -= amount;
        if (playerData.CurrentHealth <= 0)
        {
            playerData.CurrentHealth = 0;
            // OnCurrentHealthZero?.Invoke();
            base.CurrentHealthZero();
        }
    }
    public void IncreaseHealth(float amount)
    {
        playerData.CurrentHealth = Mathf.Clamp(playerData.CurrentHealth + amount, 0, playerData.MaxHealth);
        if (playerData.CurrentHealth == playerData.MaxHealth)
        {
            base.CurrentHealthFull();
        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        playerData.MaxHealth += amount;

    }

}
