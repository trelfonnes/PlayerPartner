using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : Stats, IHealthChange //interfaces for decreasing health and increasing
{//CANNOT be a child object of Stats. Has to be a child object of coreHandler or else error is thrown.
 //hold events in functions within stats and call them where needed in respective scripts 

   [SerializeField] private HeartDisplayUI heartDisplayUI;
    protected override void Awake()
    {
        base.Awake();
        UpdateUI();

    }
    private void OnEnable()
    {
        UpdateUI();
    }

    public void DecreaseHealth(float amount)
    {
        SOData.CurrentHealth -= amount;
        if (SOData.CurrentHealth <= 0)
        {
            SOData.CurrentHealth = 0;
            base.CurrentHealthZero();
        }
        UpdateUI();
    }

    
    public void IncreaseHealth(float amount)
    {
        if (!SOData.IsInjured)
        {
            SOData.CurrentHealth = Mathf.Clamp(SOData.CurrentHealth + amount, 0, SOData.MaxHealth);
            Debug.Log(SOData.CurrentHealth);
            if (SOData.CurrentHealth == SOData.MaxHealth)
            {
                base.CurrentHealthFull();
            }
            UpdateUI();
        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        SOData.MaxHealth = Mathf.Clamp(SOData.MaxHealth + amount, 0, SOData.HealthLimit);
        SOData.CurrentHealth = SOData.MaxHealth;
        UpdateUI();
    }

    protected override void Start()
    {
        base.Start();
        ClockManager.OnTick += delegate (object sender, ClockManager.OnTickEventArgs e)
        {
           
             if (SOData.IsInjured && SOData.CurrentHealth >= 2)
            {
                DecreaseHealth(2);
            }
        };
    }
    private void OnDisable()
    {
        ClockManager.OnTick -= delegate (object sender, ClockManager.OnTickEventArgs e)
        {

            if (SOData.IsInjured && SOData.CurrentHealth <= 2)
            {
                DecreaseHealth(2);
            }
        };
    }

    private void UpdateUI()
    {
        heartDisplayUI.UpdateHeartDisplay(SOData.CurrentHealth, SOData.MaxHealth);
    }

}
