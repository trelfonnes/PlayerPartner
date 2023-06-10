using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poise : Stats, IPoise
{
  

    [SerializeField] private bool regeneratePoise = false;
    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
      
        onCurrentPoiseZero += SetRegeneratePoiseTrue;
    }

   
    public void DecreasePoise(float amount)
    {
        SOData.Poise -= amount;
        if(SOData.Poise <= 0)
        {
            SOData.Poise = 0;
            base.CurrentPoiseZero();
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
        SOData.Poise = Mathf.Clamp(SOData.Poise + Time.deltaTime, 0, SOData.MaxPoise);
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
        onCurrentPoiseZero -= SetRegeneratePoiseTrue;

    }

}
