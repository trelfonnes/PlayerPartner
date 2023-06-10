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
                DecreaseStamina(1);
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
                base.CurrentStaminaZero();
            }
        
    }
    public void IncreaseStamina(float amount)
    {
        
            SOData.Stamina = Mathf.Clamp(SOData.Stamina + amount, 0, SOData.MaxStamina);
            Debug.Log(SOData.Stamina);

            if (SOData.Stamina == SOData.MaxStamina)
            {
                base.CurrentStaminaFull();
            }
        
    }
    public void IncreaseMaxStamina(float amount)
    {
        
            SOData.MaxStamina += amount;
            Debug.Log(SOData.MaxStamina);
        
    }

}
