using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Stats, IStaminaChange
{
   

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        
            base.Start();
            ClockManager.OnTick += delegate (object sender, ClockManager.OnTickEventArgs e)
            {
                if (!SOData.IsSick)
                {
                    DecreaseStamina(1);
                }
                else if (SOData.IsSick)
                {
                    DecreaseStamina(10);
                }
            };
            ClockManager.OnTick_6 += delegate (object sender, ClockManager.OnTickEventArgs e)
            {
            // Debug.Log("1/4 day passed!");
        };
        
    }
    public void DecreaseStamina(float amount)
    {
        

            SOData.Stamina -= amount;
            if (SOData.Stamina <= 0)
            {
                SOData.Stamina = 0;
            Debug.Log("Current attack power is .75%");
            //TODO: make reference to attack data when created... Maybe just an addition to the playerSOData to prevent multiple saves and issues with coordinating more SO data containers.    
            //base.CurrentStaminaZero();
            }
             else if( SOData.Stamina > 0 && SOData.Stamina < SOData.MaxStamina)
            {
                Debug.Log("Current attack is at base attack levels.");
            }
        
    }
    public void IncreaseStamina(float amount)
    {
        if (!SOData.IsSick)
        {
            SOData.Stamina = Mathf.Clamp(SOData.Stamina + amount, 0, SOData.MaxStamina);
            Debug.Log(SOData.Stamina);

            if (SOData.Stamina == SOData.MaxStamina)
            {
                Debug.Log("current attack power is 1.25%");
                base.CurrentStaminaFull();
            }
            else if( SOData.Stamina > 0 && SOData.Stamina < SOData.MaxStamina)
            {
                Debug.Log("Current attack is at base attack levels.");
            }
        }
    }
    public void IncreaseMaxStamina(float amount)
    {

        SOData.MaxStamina = Mathf.Clamp(SOData.MaxStamina + amount, 0, SOData.StaminaLimit);
        
            
        
    }
    private void OnDisable()
    {
        ClockManager.OnTick -= delegate (object sender, ClockManager.OnTickEventArgs e)
        {
            if (!SOData.IsSick)
            {
                DecreaseStamina(1);
            }
            else if (SOData.IsSick)
            {
                DecreaseStamina(10);
            }
        };
        ClockManager.OnTick_6 -= delegate (object sender, ClockManager.OnTickEventArgs e)
        {
            // Debug.Log("1/4 day passed!");
        };

    }

}
