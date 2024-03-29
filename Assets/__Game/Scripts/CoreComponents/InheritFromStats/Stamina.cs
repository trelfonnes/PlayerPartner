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
        statEvents.onCurrentHealthZero += Partner1Defeated;
        UpdateUI();
        UpdateConditionUI();
        if (gameObject.transform.parent.parent.gameObject.activeSelf)
        {
            SubscribeToHourlyTickEvent();
            
        }

    }
    protected override void Start()
    {
        
            base.Start();
       
        
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
        statEvents.onCurrentHealthZero -= Partner1Defeated;
        UnSubscribeToHourlyTickEvent();
    }
    void UpdateUI()
    {
        if (gameObject.transform.parent.parent.gameObject.activeSelf)
        {
            if (staminaDisplay != null)
                staminaDisplay.UpdateStaminaDisplay(SOData.Stamina, SOData.MaxStamina);

        }
    }
    void HandleHourlyTick(object sender, ClockManager.OnTickEventArgs e)
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
        if(SOData.Stamina == 0)
        {
            if(SOData.CurrentHealth > 1)
            SOData.CurrentHealth -= .25f;
        }

    }
        void SubscribeToHourlyTickEvent()
    {
        ClockManager.OnTick += HandleHourlyTick;

    }
    
    void UnSubscribeToHourlyTickEvent()
    {
        ClockManager.OnTick -= HandleHourlyTick;
    }
   
    void Partner1Defeated()
    {
        DecreaseStamina(SOData.MaxStamina);
    }
}
