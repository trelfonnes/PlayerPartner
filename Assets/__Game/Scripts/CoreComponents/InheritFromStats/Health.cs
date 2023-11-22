using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : Stats, IHealthChange //interfaces for decreasing health and increasing
{//CANNOT be a child object of Stats. Has to be a child object of coreHandler or else error is thrown.
 //hold events in functions within stats and call them where needed in respective scripts 
   [SerializeField] public DefensiveType defensiveType;
   [SerializeField] private HeartDisplayUI heartDisplayUI;
    protected override void Awake()
    {
        base.Awake();
        UpdateUI();

    }
    private void OnEnable()
    {
        onRevivedAndRestored += UpdateUI;
        UpdateUI();
    }

    public void DecreaseHealth(float amount)
    {
        if (gameObject.transform.parent.parent.gameObject.activeSelf)//even if only UPdateUI on active, event in stats still triggers and changes UI
        {
            SOData.CurrentHealth -= amount;
            if (SOData.CurrentHealth <= 0)
            {
                if (SOData.isPlayer)
                {
                    SOData.CurrentHealth = 0;
                    base.CurrentPlayerHealthZero();
                }
                else
                {
                    PlayerData.Instance.ep = .5f;
                    SOData.CurrentHealth = 0;
                    base.CurrentHealthZero();
                }
            }

            UpdateUI();
            UpdateConditionUI();
        }
        
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
            UpdateConditionUI();
        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        SOData.MaxHealth = Mathf.Clamp(SOData.MaxHealth + amount, 0, SOData.HealthLimit);
        SOData.CurrentHealth = SOData.MaxHealth;
        UpdateUI();
        UpdateConditionUI();
    }

    protected override void Start()
    {
        base.Start();
        if (gameObject.transform.parent.parent.gameObject.activeSelf)
        {
            SubscribeToHourlyTickEvent();
        }

    }
        private void OnDisable()
    {
        onRevivedAndRestored -= UpdateUI;
        UnSubscribeToHourlyTickEvent();
    }

    private void UpdateUI()
    {
        if (gameObject.transform.parent.parent.gameObject.activeSelf)
        {
            
            if (heartDisplayUI != null)
                heartDisplayUI.UpdateHeartDisplay(SOData.CurrentHealth, SOData.MaxHealth);
        }
    }
    void HandleHourlyTick(object sender, ClockManager.OnTickEventArgs e)
    {
        
            
            if (SOData.IsInjured)
            {
                DecreaseHealth(1);
            }
        

    }
    
    void SubscribeToHourlyTickEvent()
    {
        ClockManager.OnTick += HandleHourlyTick;

    }
   
    void UnSubscribeToHourlyTickEvent()
    {
        ClockManager.OnTick -= HandleHourlyTick;
    }
  
}
