using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplayUI : DataReferenceInheritor
{
    [SerializeField] Image staminaMeter;
    float MaxStamina = 50;
    protected override void Awake()
    {
        base.Awake();
        partner1SOData.OnStaminaValueChanged += UpdateDisplayViaInventory;
        partner2SOData.OnStaminaValueChanged += UpdateDisplayViaInventory;
        partner3SOData.OnStaminaValueChanged += UpdateDisplayViaInventory;
            
    }

    private void UpdateDisplayViaInventory(float amount)
    {
        float fillAmount = amount/MaxStamina;//using this variable because Idk how else and i'm sick and tired of this inventory system
        if(staminaMeter != null)
        {
            staminaMeter.fillAmount = fillAmount;
        }
    
    }

    public void UpdateStaminaDisplay(float currentStamina, float maxStamina)
    {
        MaxStamina = maxStamina;
        float fillAmount = currentStamina / maxStamina;
        if (staminaMeter != null)
        {
            staminaMeter.fillAmount = fillAmount;
        }
    }

    private void OnDisable()
    {
        partner1SOData.OnStaminaValueChanged -= UpdateDisplayViaInventory;
        partner2SOData.OnStaminaValueChanged -= UpdateDisplayViaInventory;
        partner3SOData.OnStaminaValueChanged -= UpdateDisplayViaInventory;

    }
}
