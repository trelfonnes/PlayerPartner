using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : Stats //interfaces for decreasing health and increasing
{//CANNOT be a child object of Stats. Has to be a child object of coreHandler or else error is thrown.
    //hold events in functions within stats and call them where needed in respective scripts 
    protected override void Awake()
    {
        base.Awake();
        playerData.currentHealth = playerData.maxHealth;
        Debug.Log(playerData.currentHealth);

    }
    public void DecreaseHealth(float amount)
    {
        playerData.currentHealth -= amount;
        if (playerData.currentHealth <= 0)
        {
            playerData.currentHealth = 0;
            // OnCurrentHealthZero?.Invoke();
            base.CurrentHealthZero();
        }
    }
    public void IncreaseHealth(float amount)
    {
        playerData.currentHealth = Mathf.Clamp(playerData.currentHealth + amount, 0, playerData.maxHealth);
    }
    public void IncreaseMaxHealth(float amount)
    {
        playerData.maxHealth = +amount;
    }

}
