using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bytes : Stats, IBytes
{
    [SerializeField] inventoryItems inventoryData;
    protected override void Awake()
    {
        base.Awake();
    }



    public void IncreaseBytes(int amount)
    {
        SOData.Bytes = Mathf.Clamp(SOData.Bytes + amount, 0, SOData.MaxBytes);
        inventoryData.numberHeld = Mathf.Clamp(inventoryData.numberHeld + amount, 0, SOData.MaxBytes);
        AddItemToInventory(inventoryData);
    }
    public void DecreaseBytes(int amount)
    {
        SOData.Bytes -= amount;
        inventoryData.numberHeld -= amount;
        if (SOData.Bytes <= 0)
        {
            SOData.Bytes = 0;
            inventoryData.numberHeld = 0;
        }
    }
    public void IncreaseMaxBytes(int amount)
    {
        SOData.MaxBytes += amount;
    }

}
