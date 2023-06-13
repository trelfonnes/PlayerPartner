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

    protected override void Awake()
    {
        //playerData = new PlayerData();
        
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
        if(onCurrentHealthZero != null)
        onCurrentHealthZero?.Invoke();
    }

    protected virtual void CurrentHealthFull()
    {   if(onCurrentHealthFull != null)
        onCurrentHealthFull.Invoke();
    }
    protected virtual void CurrentEPZero()
    {
        if (onCurrentEPZero != null)
            onCurrentEPZero.Invoke();
    }
    protected virtual void IsInjured()
    {
        if(onInjured != null)
        onInjured.Invoke();

    }
    protected virtual void IsSick()
    {
        if (onSick != null)
            onSick.Invoke();
    }

    protected virtual void CurrentStaminaFull()
    {
        if (onCurrentStaminaFull != null)
        onCurrentStaminaFull.Invoke();
    }
    protected virtual void CurrentStaminaZero()
    {
        if(onCurrentStaminaZero != null)
        onCurrentStaminaZero.Invoke();
    }
    protected virtual void CurrentPoiseZero()
    {
        if(onCurrentPoiseZero != null)
        onCurrentPoiseZero.Invoke();
    }
    #endregion


    
}
