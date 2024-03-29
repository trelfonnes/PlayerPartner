using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bytes : Stats, IBytes
{
    [SerializeField] BytesDisplayUI BytesDisplay;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        UpdateUI();

    }


    public int GetBytesAmount()
    {
        return SOData.Bytes;
    }
    public void IncreaseBytes(int amount)
    {
        SOData.Bytes = Mathf.Clamp(SOData.Bytes + amount, 0, SOData.MaxBytes);
        UpdateUI();
    }

  
    public void DecreaseBytes(int amount)
    {
        SOData.Bytes = Mathf.Clamp(SOData.Bytes - amount, 0, SOData.MaxBytes);
        UpdateUI();
      
    }
    public void IncreaseMaxBytes(int amount)
    {
        SOData.MaxBytes += amount;
    }
    private void UpdateUI()
    {
        BytesDisplay.ChangeByteDisplay(SOData.Bytes);
    }


}
