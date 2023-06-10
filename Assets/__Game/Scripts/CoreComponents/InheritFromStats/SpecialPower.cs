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
        SOData.SP -= amount;
        if(SOData.SP <= 0)
        {
            SOData.SP = 0;
        }
    }
    public void IncreaseSP(int amount)
    {
        SOData.SP = Mathf.Clamp(SOData.SP + amount, 0, SOData.MaxSP);
    }
    public void IncreaseMaxSP(int amount)
    {
        SOData.MaxSP += amount;
    }
}
