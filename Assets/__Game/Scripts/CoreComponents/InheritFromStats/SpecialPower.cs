using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPower : Stats, ISpecialPower
{
    protected override void Awake()
    {
        base.Awake();
    }
    public void DecreaseSP(int amount)
    {
        playerData.SP -= amount;
        if(playerData.SP <= 0)
        {
            playerData.SP = 0;
        }
    }
    public void IncreaseSP(int amount)
    {
        playerData.SP = Mathf.Clamp(playerData.SP + amount, 0, playerData.MaxSP);
    }
    public void IncreaseMaxSP(int amount)
    {
        playerData.MaxSP += amount;
    }
}
