using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionPower : Stats, IEvolutionPower
{
    [SerializeField] private bool startEvolutionTimer = false;
    [SerializeField] inventoryItems inventoryData;
    PlayerData _playerData;

    protected override void Awake()
    {
        base.Awake();
        _playerData = PlayerData.Instance;

    }
    protected override void Start()
    {
        base.Start();
       
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
        _playerData.ep = Mathf.Clamp(_playerData.ep - Time.deltaTime, 0, _playerData.maxEp);
      //  Debug.Log(Math.Round(_playerData.ep));//use this Math.round to display the number on the UI
        if(_playerData.ep <= 0)
        {

            StopEvolutionTimer();
            base.CurrentEPZero();
        }
    }

    public void DecreaseEP(int amount)
    {
        _playerData.ep -= amount;
        inventoryData.numberHeld -= amount;
        if(_playerData.ep <= 0)
        {
            inventoryData.numberHeld = 0;
            base.CurrentEPZero();
            StopEvolutionTimer();
        }
    }

    public void IncreaseEP(int amount)
    {
        _playerData.ep = Mathf.Clamp(_playerData.ep + amount, 0, _playerData.maxEp);
        inventoryData.numberHeld = Mathf.Clamp(inventoryData.numberHeld + amount, 0, (int)_playerData.maxEp);
        AddItemToInventory(inventoryData);

    }
    public void IncreaseMaxEP(int amount)
    {
        _playerData.maxEp += amount;
    }
    public void StartEvolutionTimer()
    {
        startEvolutionTimer = true;
    }
    public void StopEvolutionTimer()
    {
        startEvolutionTimer = false;
    }
}
