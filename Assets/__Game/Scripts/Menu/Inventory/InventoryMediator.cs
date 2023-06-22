using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMediator: MonoBehaviour
{
    [SerializeField] PlayerSOData player;
    [SerializeField] PlayerSOData partner1;
    [SerializeField] PlayerSOData partner2;
    [SerializeField] PlayerSOData partner3;
    
    
    // not sure how to get all partner data referenced other than hard reference.
    //this system works purely with prefabs, nothing in the actual scene.


    public void IncreaseStamina(float amount)
    {
        if (partner1)
        {
            partner1.Stamina = Mathf.Clamp(partner1.Stamina + amount, 0, partner1.MaxStamina);
            partner2.Stamina = Mathf.Clamp(partner2.Stamina + amount, 0, partner2.MaxStamina);
            partner3.Stamina = Mathf.Clamp(partner3.Stamina + amount, 0, partner3.MaxStamina);

        }
    }
    public void IncreasePartnerHealth(float amount)
    {
        if (partner1)
        {
            partner1.CurrentHealth = Mathf.Clamp(partner1.CurrentHealth + amount, 0, partner1.MaxHealth);
            partner2.CurrentHealth = Mathf.Clamp(partner2.CurrentHealth + amount, 0, partner2.MaxHealth);
            partner3.CurrentHealth = Mathf.Clamp(partner3.CurrentHealth + amount, 0, partner3.MaxHealth);
        }
        
    }
    public void IncreasePlayerHealth(float amount)
    {
        Debug.Log("Health UP");
        if (player)
        {
            player.CurrentHealth = Mathf.Clamp(player.CurrentHealth + amount, 0, player.MaxHealth);

        }
    }
    public void HealSick( bool sick)
    {

        if (partner1)
        {
            partner1.Sick = sick;
            partner2.Sick = sick;
            partner3.Sick = sick;
        }
    }
    public void HealInjured(bool injured)
    {
        if (partner1)
        {
            partner1.Injured = injured;    
            partner2.Injured = injured;    
            partner3.Injured = injured;    
        }
    }
   
   
}
