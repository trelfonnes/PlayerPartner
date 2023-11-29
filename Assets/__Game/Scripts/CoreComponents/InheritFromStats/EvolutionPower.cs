using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionPower : Stats, IEvolutionPower
{
    [SerializeField] private bool startEvolutionTimer = false;
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
    void EPHealthZeroPenalty()
    {
        DecreaseEP(playerData.maxEp);
    }
    public void DecreaseEP(float amount)
    {
        playerData.ep -= amount;
        if(playerData.ep <= 0)
        {
            playerData.ep = 0;
            StopEvolutionTimer();
        }
        roundedAmount = Mathf.RoundToInt(playerData.ep);
        EPDisplay.UpdateEPDisplayUI(roundedAmount);
    }

    public void IncreaseEP(int amount)
    {
        playerData.ep = Mathf.Clamp(playerData.ep + amount, 0, playerData.maxEp);
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
    private void OnEnable()
    {
        statEvents.onCurrentHealthZero += EPHealthZeroPenalty;

    }
    private void OnDisable()
    {
        statEvents.onCurrentHealthZero -= EPHealthZeroPenalty;

    }
}
