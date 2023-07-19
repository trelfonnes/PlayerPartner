using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionPower : Stats, IEvolutionPower
{
    [SerializeField] private bool startEvolutionTimer = false;
    [SerializeField] inventoryItems inventoryData;
    [SerializeField] EPDisplayUI EPDisplay;
    int roundedAmount;

    protected override void Awake()
    {
        base.Awake();

    }
    protected override void Start()
    {
        base.Start();
       
    }

    public void DecreaseEP(int amount)
    {
        playerData.ep -= amount;
        inventoryData.numberHeld -= amount;
        if(playerData.ep <= 0)
        {
            inventoryData.numberHeld = 0;
            
            StopEvolutionTimer();
        }
        roundedAmount = Mathf.RoundToInt(playerData.ep);
        EPDisplay.UpdateEPDisplayUI(roundedAmount);
    }

    public void IncreaseEP(int amount)
    {
        playerData.ep = Mathf.Clamp(playerData.ep + amount, 0, playerData.maxEp);
        inventoryData.numberHeld = Mathf.Clamp(inventoryData.numberHeld + amount, 0, (int)playerData.maxEp);
        AddItemToInventory(inventoryData);
        roundedAmount = Mathf.RoundToInt(playerData.ep);
        EPDisplay.UpdateEPDisplayUI(roundedAmount);
    }
    public void IncreaseMaxEP(int amount)
    {
        playerData.maxEp += amount;
    }
    public void StartEvolutionTimer()
    {
        playerData.StartEPTimer = true;
    }
    public void StopEvolutionTimer()
    {
        playerData.StartEPTimer = false;
    }
}
