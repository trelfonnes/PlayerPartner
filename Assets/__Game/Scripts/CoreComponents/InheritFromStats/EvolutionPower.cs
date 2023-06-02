using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionPower : Stats, IEvolutionPower
{

    protected override void Awake()
    {
        base.Awake();
    }
    public void DecreaseEP(int amount)
    {
        playerData.EP -= amount;
        if(playerData.EP <= 0)
        {
            playerData.EP = 0;
            base.CurrentEPZero(); 
        }
    }

    public void IncreaseEP(int amount)
    {
        playerData.EP = Mathf.Clamp(playerData.EP + amount, 0, playerData.MaxEP);

    }
    public void IncreaseMaxEP(int amount)
    {
        playerData.MaxEP += amount;
    }
}
