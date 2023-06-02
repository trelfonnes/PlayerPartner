using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionPower : Stats, IEvolutionPower
{
    private bool startEvolutionTimer = false;

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
        playerData.EP = Mathf.Clamp(playerData.EP - Time.deltaTime, 0, playerData.MaxEP);
        Debug.Log(playerData.EP);
        if(playerData.EP <= 0)
        {
            startEvolutionTimer = false;
            base.CurrentEPZero();
        }
    }

    public void DecreaseEP(int amount)
    {
        playerData.EP -= amount;
        if(playerData.EP <= 0)
        {
            playerData.EP = 0;
            base.CurrentEPZero(); 
        }
    }

    public void IncreaseEP(int amount)
    {
        playerData.EP = Mathf.Clamp(playerData.EP + amount, 0, playerData.MaxEP);

    }
    public void IncreaseMaxEP(int amount)
    {
        playerData.MaxEP += amount;
    }
    public void StartEvolutionTimer()
    {
        startEvolutionTimer = true;
    }
}
