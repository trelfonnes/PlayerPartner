using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poise : Stats, IPoiseDamageable
{
  

    [SerializeField] private bool regeneratePoise = false;
    [SerializeField] private float poiseRecoveryRate = 2f;
    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        statEvents.onCurrentPoiseZero += SetRegeneratePoiseTrue;
    }

   
    public void DecreasePoise(float amount)
    {
        SOData.Poise -= amount;
        if(SOData.Poise <= 0)
        {
            SOData.Poise = 0;
            base.CurrentPoiseZero();  //TODO: make sure to subscribe to this stat event in enemy (andPartner) state machine so they can enter "stun State"
        }
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (regeneratePoise)
        {
            RegeneratePoise();
        }


    }
    public void RegeneratePoise()
    {
        SOData.Poise = Mathf.Clamp(SOData.Poise + (Time.deltaTime * poiseRecoveryRate), 0, SOData.MaxPoise);
        if(SOData.Poise == SOData.MaxPoise)
        {
            regeneratePoise = false;
        }    
    }
    private void SetRegeneratePoiseTrue()
    {
        regeneratePoise = true;
    }
    private void OnDisable()
    {
        statEvents.onCurrentPoiseZero -= SetRegeneratePoiseTrue;

    }

}
