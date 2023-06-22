using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMediator: DataReferenceInheritor
{
  

    protected override void Awake()
    {
        base.Awake();
        


    }

    // not sure how to get all partner data referenced other than hard reference.
    //this system works purely with prefabs, nothing in the actual scene.


    public void IncreaseStamina(float amount)
    {
        if (partner1SOData)
        {
            partner1SOData.Stamina = Mathf.Clamp(partner1SOData.Stamina + amount, 0, partner1SOData.MaxStamina);
            partner2SOData.Stamina = Mathf.Clamp(partner2SOData.Stamina + amount, 0, partner2SOData.MaxStamina);
            partner3SOData.Stamina = Mathf.Clamp(partner3SOData.Stamina + amount, 0, partner3SOData.MaxStamina);

        }
    }
    public void IncreasePartnerHealth(float amount)
    {
        if (partner1SOData)
        {
            partner1SOData.CurrentHealth = Mathf.Clamp(partner1SOData.CurrentHealth + amount, 0, partner1SOData.MaxHealth);
            partner2SOData.CurrentHealth = Mathf.Clamp(partner2SOData.CurrentHealth + amount, 0, partner2SOData.MaxHealth);
            partner3SOData.CurrentHealth = Mathf.Clamp(partner3SOData.CurrentHealth + amount, 0, partner3SOData.MaxHealth);
        }
        
    }
    public void IncreasePlayerHealth(float amount)
    {
        Debug.Log("Health UP");
        if (playerSOData)
        {
            playerSOData.CurrentHealth = Mathf.Clamp(playerSOData.CurrentHealth + amount, 0, playerSOData.MaxHealth);

        }
    }
    public void HealSick( bool sick)
    {

        if (partner1SOData)
        {
            partner1SOData.IsSick = sick;
            partner2SOData.IsSick = sick;
            partner3SOData.IsSick = sick;
        }
    }
    public void HealInjured(bool injured)
    {
        if (partner1SOData)
        {
            partner1SOData.IsInjured = injured;    
            partner2SOData.IsInjured = injured;    
            partner3SOData.IsInjured = injured;    
        }
    }
   
   
}
