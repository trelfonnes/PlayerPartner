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
        Stats.onCurrentHealthZero += SetRegeneratePoiseTrue;
    }

   
    public void DecreasePoise(float amount)
    {
        playerData.Poise -= amount;
        if(playerData.Poise <= 0)
        {
            playerData.Poise = 0;
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
        playerData.Poise = Mathf.Clamp(playerData.Poise + Time.deltaTime, 0, playerData.MaxPoise);
        if(playerData.Poise == playerData.MaxPoise)
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
        Stats.onCurrentHealthZero -= SetRegeneratePoiseTrue;

    }

}
