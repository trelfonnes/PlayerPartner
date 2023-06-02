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
        playerData.Bytes = Mathf.Clamp(playerData.Bytes + amount, 0, playerData.MaxBytes);

    }
    public void DecreaseBytes(int amount)
    {
        playerData.Bytes -= amount;
        if (playerData.Bytes <= 0)
        {
            playerData.Bytes = 0;
        }
    }
    public void IncreaseMaxBytes(int amount)
    {
        playerData.MaxBytes += amount;
    }

}
