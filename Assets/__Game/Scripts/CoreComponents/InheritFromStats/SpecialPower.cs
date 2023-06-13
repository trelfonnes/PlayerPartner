using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPower : Stats, ISpecialPower
{
    [SerializeField] inventoryItems inventoryData;
    protected override void Awake()
    {
        base.Awake();
    }
    public void DecreaseSP(int amount)
    {
        SOData.SP -= amount;
        inventoryData.numberHeld -= amount;
        if(SOData.SP <= 0)
        {
            SOData.SP = 0;
            inventoryData.numberHeld = 0;
        }
    }
    public void IncreaseSP(int amount)
    {
        SOData.SP = Mathf.Clamp(SOData.SP + amount, 0, SOData.MaxSP);
        inventoryData.numberHeld = Mathf.Clamp(inventoryData.numberHeld + amount, 0, SOData.MaxSP);
        AddItemToInventory(inventoryData);

    }
    public void IncreaseMaxSP(int amount)
    {
        SOData.MaxSP += amount;
    }
}
