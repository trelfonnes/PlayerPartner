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
        partner2SOData.OnStaminaValueChanged += UpdateDisplayViaInventory;
        partner3SOData.OnStaminaValueChanged += UpdateDisplayViaInventory;
        partner1SOData.OnStaminaValueChanged += UpdateDisplayViaInventory;

    }

    private void UpdateDisplayViaInventory(float amount)
    {
        if (staminaMeter != null)
        {
            Debug.Log("updating via data, not through stats");
            float fillAmount = amount/MaxStamina;//using this variable because Idk how else and i'm sick and tired of this inventory system
        
            staminaMeter.fillAmount = fillAmount;
        }
    
    }

    public void UpdateStaminaDisplay(float currentStamina, float maxStamina)
    {
        if (staminaMeter != null)
        {
            MaxStamina = maxStamina;
        float fillAmount = currentStamina / maxStamina;
        
            staminaMeter.fillAmount = fillAmount;
        }
    }

    private void OnDisable()
    {
        partner2SOData.OnStaminaValueChanged -= UpdateDisplayViaInventory;
        partner3SOData.OnStaminaValueChanged -= UpdateDisplayViaInventory;
        partner1SOData.OnStaminaValueChanged -= UpdateDisplayViaInventory;

    }
}
