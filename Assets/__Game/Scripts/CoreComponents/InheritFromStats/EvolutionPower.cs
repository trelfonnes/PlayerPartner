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
        DecreaseEP(PlayerData.Instance.maxEp);
    }
    public void DecreaseEP(float amount)
    {
        PlayerData.Instance.ep -= amount;
        if(PlayerData.Instance.ep <= 0)
        {
            PlayerData.Instance.ep = 0;
            StopEvolutionTimer();
        }
        roundedAmount = Mathf.RoundToInt(PlayerData.Instance.ep);
        EPDisplay.UpdateEPDisplayUI(roundedAmount);
    }

    public void IncreaseEP(int amount)
    {
        PlayerData.Instance.ep = Mathf.Clamp(PlayerData.Instance.ep + amount, 0, PlayerData.Instance.maxEp);
        roundedAmount = Mathf.RoundToInt(PlayerData.Instance.ep);
        EPDisplay.UpdateEPDisplayUI(roundedAmount);
    }
    public void IncreaseMaxEP(int amount)
    {
        PlayerData.Instance.maxEp += amount;
    }
    public void StartEvolutionTimer()
    {
        PlayerData.Instance.StartEPTimer = true;
    }
    public void StopEvolutionTimer()
    {
        PlayerData.Instance.StartEPTimer = false;
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
