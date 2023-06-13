using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionPower : Stats, IEvolutionPower
{
    [SerializeField] private bool startEvolutionTimer = false;
    [SerializeField] inventoryItems inventoryData;


    protected override void Awake()
    {
        base.Awake();
        
    }
    protected override void Start()
    {
        base.Start();
        //TODO: subscribe to event located in animator?? player.anim.onEvolved += StartEvolutionTimer;
        // dont forget to unsubscribe!!
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (startEvolutionTimer)
        {
            EvolutionPowerCountDown();
        }

    }

    private void EvolutionPowerCountDown()
    {
        SOData.EP = Mathf.Clamp(SOData.EP - Time.deltaTime, 0, SOData.MaxEP);
        Debug.Log(Math.Round(SOData.EP));//use this Math.round to display the number on the UI
        if(SOData.EP <= 0)
        {
            startEvolutionTimer = false;
            base.CurrentEPZero();
        }
    }

    public void DecreaseEP(int amount)
    {
        SOData.EP -= amount;
        inventoryData.numberHeld -= amount;
        if(SOData.EP <= 0)
        {
            SOData.EP = 0;
            inventoryData.numberHeld = 0;
            base.CurrentEPZero(); 
        }
    }

    public void IncreaseEP(int amount)
    {
        SOData.EP = Mathf.Clamp(SOData.EP + amount, 0, SOData.MaxEP);
        inventoryData.numberHeld = Mathf.Clamp(inventoryData.numberHeld + amount, 0, (int)SOData.MaxEP);
        AddItemToInventory(inventoryData);

    }
    public void IncreaseMaxEP(int amount)
    {
        SOData.MaxEP += amount;
    }
    public void StartEvolutionTimer()
    {
        startEvolutionTimer = true;
    }
}
