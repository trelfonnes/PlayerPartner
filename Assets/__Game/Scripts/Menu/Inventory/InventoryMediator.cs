using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMediator: MonoBehaviour
{

    [SerializeField] List<PlayerSOData> Datas = new List<PlayerSOData>();

    
    // not sure how to get all partner data referenced other than hard reference.
    //this system works purely with prefabs, nothing in the actual scene.


    public void IncreaseStamina(float amount)
    {
        if (Datas != null)
        {
            foreach (PlayerSOData item in Datas)
            {
                item.Stamina = Mathf.Clamp(item.Stamina + amount, 0, item.MaxStamina);
            }
            
        }
    }
    public void IncreasePartnerHealth(float amount)
    {
        if (Datas != null)
        {

            for (int i = 0; i < Datas.Count; i++)
            {
                if (i != 0) // 0 is the first slot in the list which will always be for the player
                {
                    Datas[i].CurrentHealth = Mathf.Clamp(Datas[i].CurrentHealth + amount, 0, Datas[i].MaxHealth);
                }

            }
        }    
        
    }
    public void IncreasePlayerHealth(float amount)
    {
        Debug.Log("Health UP");
        if (Datas != null)
        {
            for (int i = 0; i < Datas.Count; i++)
            {
                if (i == 0)
                {
                    Datas[i].CurrentHealth = Mathf.Clamp(Datas[i].CurrentHealth + amount, 0, Datas[i].MaxHealth);
                }
            }
        }
    }
    public void HealSick( bool sick)
    {
        if (Datas != null)
        {

            foreach (PlayerSOData item in Datas)
            {
                item.IsSick = sick;
            }
        }
    }
    public void HealInjured(bool injured)
    {
        if (Datas != null)
        {
            foreach (PlayerSOData item in Datas)
            {
                item.IsInjured = injured;
            }

        }
    }
   
   
}
