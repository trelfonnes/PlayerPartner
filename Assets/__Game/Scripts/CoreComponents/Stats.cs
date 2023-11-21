using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : CoreComponent, IInventory
{
    List<int> conditionIndices = new List<int>();
    protected PlayerData playerData;
    [SerializeField]
    protected ConditionDisplayUI conditionDisplay;
    [SerializeField]
    protected PlayerSOData SOData;//Data for states  
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] public StatEvents statEvents;
    // protected PlayerData _playerData = PlayerData.Instance;
    public delegate void RevivedAndRestoredEventHandler();
    protected event Action onRevivedAndRestored;


    protected override void Awake()
    {

        playerData = PlayerData.Instance;
        UpdateConditionUI();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    protected void UpdateConditionUI()
    {
        if (gameObject.activeSelf)
        {
            if (conditionDisplay != null)
            {
                Dictionary<int, bool> conditionData = new Dictionary<int, bool>();

                // Add conditions to the dictionary based on their corresponding checks
                conditionData.Add(0, SOData.CurrentHealth == SOData.MaxHealth && !SOData.IsSick && !SOData.IsInjured);
                conditionData.Add(1, SOData.IsSick);
                conditionData.Add(2, SOData.IsInjured);
                conditionData.Add(3, SOData.Stamina == 0);
                conditionData.Add(4, SOData.CurrentHealth <= 2);

                // Create a list to store the condition indices to be passed to the UpdateConditionUI method
                List<int> conditionIndices = new List<int>();

                // Iterate over the condition data and add the condition indices to the list
                foreach (KeyValuePair<int, bool> condition in conditionData)
                {
                    if (condition.Value)
                    {
                        conditionIndices.Add(condition.Key);
                    }
                }

                // Update the condition display UI
                conditionDisplay.UpdateConditionUI(conditionIndices);
            }
        }

    }
    public void AddItemToInventory(inventoryItems item)
    {
        Debug.Log(playerInventory);
        if (playerInventory)
        {

            if (playerInventory.myInventory.Contains(item))
            {

                return;
            }
            else
            {
                playerInventory.myInventory.Add(item);
                Debug.Log("AddingItem");
            }
        }
    }
    public void ReviveAndRestore()
    {
        //set everything healthy
        SOData.CurrentHealth = SOData.MaxHealth;
        PlayerData.Instance.partnerIsDefeated = false;
        SOData.Stamina = SOData.MaxStamina;
        SOData.IsInjured = false;
        SOData.IsSick = false;
        statEvents?.PartnerRestored();
        UpdateConditionUI();
        UpdateHealthAndStaminaUIFromStats();
        onRevivedAndRestored?.Invoke();

    }

    #region Events for stat changes
    protected virtual void UpdateHealthAndStaminaUIFromStats()
    {

    }

    protected virtual void CurrentHealthZero()
    {
        statEvents.CurrentHealthZero();
    }

    protected virtual void CurrentHealthFull()
    {
        statEvents.CurrentHealthFull();
    }
    
    protected virtual void IsInjured()
    {
        statEvents.IsInjured();

    }
    protected virtual void IsSick()
    {
        statEvents.IsSick();
    }

    protected virtual void CurrentStaminaFull()
    {
        statEvents.CurrentStaminaFull();
    }
    protected virtual void CurrentStaminaZero()
    {
        statEvents.CurrentStaminaZero();
    }
    protected virtual void CurrentPoiseZero()
    {
        statEvents.CurrentPoiseZero();
    }
    #endregion


    
}
