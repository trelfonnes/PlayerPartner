using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : Stats, IKeys
{
    [SerializeField] private KeyDisplayUI keyDisplay;
    [SerializeField] private KeyDisplayUI bossKeyDisplay;


    protected override void Awake()
    {
        base.Awake();
           UpdateUI();
    }
    private void OnEnable()
    {
            UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (keyDisplay != null)
        {
            keyDisplay.ChangeKeyDisplayAmount(SOData.Keys);
        }

        if(bossKeyDisplay != null)
        {
            bossKeyDisplay.ChangeKeyDisplayAmount(SOData.BossKeys);
        }
    }
    
    public int GetKeyAmount()
    {
        return SOData.Keys;
    }
    public int GetBossKeyAmount()
    {
        return SOData.BossKeys;
    }
    public void AddKey(int amount)
    {
        SOData.Keys += amount;
        UpdateUI();

    }
    public void AddBossKey(int amount)
    {
        SOData.BossKeys += amount;
        UpdateUI();

    }
    public void MinusKey(int amount)
    {
        SOData.Keys -= amount;
        if (SOData.Keys < 0)
        {
            SOData.Keys = 0;
        }
        UpdateUI();

    }
    public void MinusBossKey(int amount)
    {
        SOData.BossKeys -= amount;
        if(SOData.BossKeys < 0)
        {
            SOData.BossKeys = 0;
        }
        UpdateUI();

    }
}
