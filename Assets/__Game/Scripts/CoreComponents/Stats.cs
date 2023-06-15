using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : CoreComponent, IInventory
{
    public event Action onCurrentHealthZero;
    public event Action onCurrentHealthFull;
    public event Action onCurrentEPZero;
    public event Action onInjured;
    public event Action onSick;
    public event Action onCurrentStaminaZero;
    public event Action onCurrentStaminaFull;
    public event Action onCurrentPoiseZero;

    [SerializeField]
    protected PlayerSOData SOData;//Data for states  
    [SerializeField]  PlayerInventory playerInventory;
    [SerializeField] StatEvents statEvents;
   // protected PlayerData _playerData = PlayerData.Instance;

    protected override void Awake()
    {
      
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public void AddItemToInventory(inventoryItems item)
    {
        Debug.Log(playerInventory);
        if (playerInventory)
        {
            Debug.Log("Inside add function");

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

    #region Events for stat changes
    protected virtual void CurrentHealthZero()
    {
        statEvents.CurrentHealthZero();
    }

    protected virtual void CurrentHealthFull()
    {
        statEvents.CurrentHealthFull();
    }
    protected virtual void CurrentEPZero()
    {
        statEvents.CurrentEPZero();
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
