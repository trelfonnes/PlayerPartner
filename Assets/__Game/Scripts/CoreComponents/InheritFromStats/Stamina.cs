using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Stats, IStaminaChange
{

    [SerializeField] StaminaDisplayUI staminaDisplay;
    protected override void Awake()
    {
        base.Awake();
        UpdateUI();
    }
    private void OnEnable()
    {
        UpdateUI();
        UpdateConditionUI();

    }
    protected override void Start()
    {
        
            base.Start();
            ClockManager.OnTick += delegate (object sender, ClockManager.OnTickEventArgs e)
            {
                if (gameObject.transform.parent.parent.gameObject.activeSelf)
                {

                    if (SOData.Stamina > 0)
                    {
                        if (!SOData.IsSick)
                        {
                            DecreaseStamina(1);
                        }
                        else if (SOData.IsSick)
                        {
                            DecreaseStamina(10);
                        }
                    }
                    else
                        return;
                }
            };
            ClockManager.OnTick_6 += delegate (object sender, ClockManager.OnTickEventArgs e)
            {
            // Debug.Log("1/4 day passed!");
        };
        
    }
    public void DecreaseStamina(float amount)
    {
        if (gameObject.transform.parent.parent.gameObject.activeSelf)
        {

            SOData.Stamina -= amount;

            if (SOData.Stamina <= 0)
            {
                SOData.Stamina = 0;
                UpdateConditionUI();
                // Debug.Log("Current attack power is .75%");
                //TODO: make reference to attack data when created... Maybe just an addition to the playerSOData to prevent multiple saves and issues with coordinating more SO data containers.    
                //base.CurrentStaminaZero();
            }
            else if (SOData.Stamina > 0 && SOData.Stamina < SOData.MaxStamina)
            {
                //  Debug.Log("Current attack is at base attack levels.");
            }
            if (gameObject.activeSelf)
            {
                UpdateUI();
                UpdateConditionUI();
            }
        }
        else
            return;


    }
    public void IncreaseStamina(float amount)
    {
        if (!SOData.IsSick)
        {
            SOData.Stamina = Mathf.Clamp(SOData.Stamina + amount, 0, SOData.MaxStamina);
            Debug.Log(SOData.Stamina);

            if (SOData.Stamina == SOData.MaxStamina)
            {
              //  Debug.Log("current attack power is 1.25%");
                base.CurrentStaminaFull();
            }
            else if( SOData.Stamina > 0 && SOData.Stamina < SOData.MaxStamina)
            {
             //   Debug.Log("Current attack is at base attack levels.");
            }
            UpdateUI();
            UpdateConditionUI();
        }
    }
    public void IncreaseMaxStamina(float amount)
    {

        SOData.MaxStamina = Mathf.Clamp(SOData.MaxStamina + amount, 0, SOData.StaminaLimit);
        if (!SOData.IsSick)
        {
            SOData.Stamina = SOData.MaxStamina;
        }
        UpdateUI();
        UpdateConditionUI();



    }
    private void OnDisable()
    {
        ClockManager.OnTick -= delegate (object sender, ClockManager.OnTickEventArgs e)
        {
            
        };
        ClockManager.OnTick_6 -= delegate (object sender, ClockManager.OnTickEventArgs e)
        {
            // Debug.Log("1/4 day passed!");
        };

    }
    void UpdateUI()
    {
        if (gameObject.transform.parent.parent.gameObject.activeSelf)
        {
            if (staminaDisplay != null)
                staminaDisplay.UpdateStaminaDisplay(SOData.Stamina, SOData.MaxStamina);

        }
    }

}
