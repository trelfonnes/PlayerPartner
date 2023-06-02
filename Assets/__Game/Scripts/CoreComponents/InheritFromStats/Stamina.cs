using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Stats, IStaminaChange
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void DecreaseStamina(float amount)
    {
        playerData.Stamina -= amount;
        if(playerData.Stamina <= 0)
        {
            playerData.Stamina = 0;
            base.CurrentStaminaZero();
        }
    }
    public void IncreaseStamina(float amount)
    {
        playerData.Stamina = Mathf.Clamp(playerData.Stamina + amount, 0, playerData.MaxStamina);
        Debug.Log(playerData.Stamina);

        if (playerData.Stamina == playerData.MaxStamina)
        {
            base.CurrentStaminaFull();
        }
       
    }
    public void IncreaseMaxStamina(float amount)
    {
        playerData.MaxStamina += amount;
        Debug.Log(playerData.MaxStamina);
    }

}
