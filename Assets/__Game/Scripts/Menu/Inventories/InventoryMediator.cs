using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMediator : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    

    [SerializeField] List<PlayerSOData> PartnerDatas = new List<PlayerSOData>();
    [SerializeField] List<PlayerSOData> PlayerDatas = new List<PlayerSOData>();

    //holds all datas for communication from inventory to inside systems.
    
    private void Start()
    {
            if(inventoryManager != null)
        {
            inventoryManager.onHealthPotionUsed += IncreasePartnerHealth;
            inventoryManager.onStaminaPotionUsed += IncreaseStamina;
            inventoryManager.onPlayerPotionUsed += IncreasePlayerHealth;
            inventoryManager.onElixirUsed += ElixirUsedHealAll;
            inventoryManager.onMedicineUsed += HealSick;
            inventoryManager.onBandaidUsed += HealInjured;
        }
    }
    public void IncreaseStamina(float amount)
    {
        if (PartnerDatas != null && !PlayerData.Instance.partnerIsDefeated)
        {
            foreach (PlayerSOData item in PartnerDatas)
            {
                item.Stamina = Mathf.Clamp(item.Stamina + amount, 0, item.MaxStamina);
            }
            
        }
    }
    public void ElixirUsedHealAll(float amount)
    {
        IncreasePartnerHealth(amount);
        IncreaseStamina(amount);
        HealSick();
        HealInjured();
        IncreasePlayerHealth(amount);
       
    }
    public void IncreasePlayerHealth(float amount)
    {
        if (PlayerDatas != null)
        {

            for (int i = 0; i < PlayerDatas.Count; i++)
            {
                if (i == 0) // 0 is the first slot in the list which will always be for the player
                {
                    float adjustedAmount = Mathf.Clamp(PlayerDatas[i].CurrentHealth + amount, 0, PlayerDatas[i].MaxHealth);
                    PlayerDatas[i].SetPartnerHealthFromItem(adjustedAmount);
                }

            }
        }    
        
    }
    public void IncreasePartnerHealth(float amount)
    {
        Debug.Log("Health UP");
        if (PartnerDatas != null && !PlayerData.Instance.partnerIsDefeated)
        {
            for (int i = 0; i < PartnerDatas.Count; i++)
            {
                    float adjustedAmount = Mathf.Clamp(PartnerDatas[i].CurrentHealth + amount, 0, PartnerDatas[i].MaxHealth);
                    PartnerDatas[i].SetPartnerHealthFromItem(adjustedAmount);
              
            }
        }
    }
    public void HealSick()
    {
        if (PartnerDatas != null && !PlayerData.Instance.partnerIsDefeated)
        {

            foreach (PlayerSOData item in PartnerDatas)
            {
                if (item != null)
                {
                    item.IsSick = false;
                }
            }
        }
    }
    public void HealInjured()
    {
        if (PartnerDatas != null && !PlayerData.Instance.partnerIsDefeated)
        {
            foreach (PlayerSOData item in PartnerDatas)
            {
                if (item != null)
                {
                    item.IsInjured = false;
                }
            }

        }
    }

    private void OnDisable()
    {
        inventoryManager.onHealthPotionUsed -= IncreasePartnerHealth;
        inventoryManager.onStaminaPotionUsed -= IncreaseStamina;
        inventoryManager.onPlayerPotionUsed -= IncreasePlayerHealth;
        inventoryManager.onElixirUsed -= ElixirUsedHealAll;
        inventoryManager.onMedicineUsed -= HealSick;
        inventoryManager.onBandaidUsed -= HealInjured;
    }

}
