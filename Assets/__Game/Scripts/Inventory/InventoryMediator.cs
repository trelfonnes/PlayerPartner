using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMediator: MonoBehaviour
{
    [SerializeField] PlayerSOData player;
    [SerializeField] PlayerSOData partner;


    public void IncreaseStamina(float amount)
    {
        if (partner)
        {
            partner.Stamina = Mathf.Clamp(partner.Stamina + amount, 0, partner.MaxStamina);

        }
    }
    public void IncreasePartnerHealth(float amount)
    {
        if (partner)
        {
            partner.CurrentHealth = Mathf.Clamp(partner.CurrentHealth + amount, 0, partner.MaxHealth);
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
        if (partner)
        {
            partner.Sick = sick;
        }
    }
    public void HealInjured(bool injured)
    {
        if (partner)
        {
            partner.Injured = injured;
        }
    }
   
}
