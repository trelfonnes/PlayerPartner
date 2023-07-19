using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPower : Stats, ISpecialPower
{
    public bool canShoot = true;
    [SerializeField] SPDisplayUI SPDisplay;
    [SerializeField] inventoryItems inventoryData;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        if (SPDisplay != null)
        {
            UpdateUI();
        }
    }


    public void DecreaseSP(int amount)
    {
        SOData.SP -= amount;
        inventoryData.numberHeld -= amount;
        if(SOData.SP <= 0)
        {
            canShoot = false;
            SOData.SP = 0;
            inventoryData.numberHeld = 0;
        }
        UpdateUI();

    }
    public void IncreaseSP(int amount)
    {
        SOData.SP = Mathf.Clamp(SOData.SP + amount, 0, SOData.MaxSP);
        inventoryData.numberHeld = Mathf.Clamp(inventoryData.numberHeld + amount, 0, SOData.MaxSP);
        canShoot = true;
        AddItemToInventory(inventoryData);
        UpdateUI();

    }
    public void IncreaseMaxSP(int amount)
    {//TODO add a Mathf.Clamp for SOData.SPLimit
        SOData.MaxSP += amount;
    }
    private void UpdateUI()
    {
       
            SPDisplay.ChangeSPDisplay(SOData.SP);
        
    }

}
