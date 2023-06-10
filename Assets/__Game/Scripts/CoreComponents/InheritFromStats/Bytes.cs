using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bytes : Stats, IBytes
{
   
    protected override void Awake()
    {
        base.Awake();
    }

    public void IncreaseBytes(int amount)
    {
        SOData.Bytes = Mathf.Clamp(SOData.Bytes + amount, 0, SOData.MaxBytes);

    }
    public void DecreaseBytes(int amount)
    {
        SOData.Bytes -= amount;
        if (SOData.Bytes <= 0)
        {
            SOData.Bytes = 0;
        }
    }
    public void IncreaseMaxBytes(int amount)
    {
        SOData.MaxBytes += amount;
    }

}
