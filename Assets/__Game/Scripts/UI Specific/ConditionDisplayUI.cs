using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionDisplayUI : DataReferenceInheritor
{
    
    [SerializeField] List<Image> conditions = new List<Image>();
    //index must be in this order
    // [0] Happy
    // [1] Sick
    // [2] Injured
    // [3] Hungry
    // [4] lowHP
    protected override void Awake()
    {
        base.Awake();
      //  partner1SOData.isInjuredChanged.AddListener(UpdateConditionUIInjured);
       // partner1SOData.isSickChanged.AddListener(UpdateConditionUISick);
        partner1SOData.onInjuredChanged += UpdateConditionUIInjured;
        partner1SOData.onSickChanged += UpdateConditionUISick;
        partner1SOData.OnCurrentHealthValueChanged += UpdateHealthDisplayFromInventory;
        partner2SOData.OnCurrentHealthValueChanged += UpdateHealthDisplayFromInventory;
        partner3SOData.OnCurrentHealthValueChanged += UpdateHealthDisplayFromInventory;
        partner1SOData.OnStaminaValueChanged += UpdateStaminaDisplayFromInventory;
        partner2SOData.OnStaminaValueChanged += UpdateStaminaDisplayFromInventory;
        partner3SOData.OnStaminaValueChanged += UpdateStaminaDisplayFromInventory;

    }


    private void UpdateStaminaDisplayFromInventory(float stamina)
    {
        if(stamina > 0f)
        {
            conditions[3].gameObject.SetActive(false);
        }
    }

    private void UpdateHealthDisplayFromInventory(float currentHealth)
    {
      
            if (currentHealth > 2)
            {
                conditions[0].gameObject.SetActive(true);
                conditions[4].gameObject.SetActive(false);
            }
        
        
    }

    private void UpdateConditionUISick(bool sick)
    {
        if (conditions != null)
        {
            if (sick)
            {
                conditions[1].gameObject.SetActive(true);
            }
            else
            {
                conditions[1].gameObject.SetActive(false);
                
            }
        }
    }

    private void UpdateConditionUIInjured(bool injured)  //for changes by inventory
    {
        if (conditions != null)
        {
            Debug.Log("Set Is INjured UI" + injured);
            if (injured)
            {
                conditions[2].gameObject.SetActive(true);
            }
            else
            {
                conditions[2].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateConditionUI(List<int> conditionIndices) //for changes via stat interfaces
    {
        // Deactivate all conditions
        for (int i = 0; i < conditions.Count; i++)
        {
            conditions[i].gameObject.SetActive(false);
        }

        // Activate the specified conditions
        for (int i = 0; i < conditionIndices.Count; i++)
        {
            int conditionIndex = conditionIndices[i];
            if (conditionIndex >= 0 && conditionIndex < conditions.Count)
            {
                conditions[conditionIndex].gameObject.SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
      //  partner1SOData.isInjuredChanged.RemoveListener(UpdateConditionUIInjured);
       // partner1SOData.isSickChanged.RemoveListener(UpdateConditionUISick);
        partner1SOData.OnCurrentHealthValueChanged -= UpdateHealthDisplayFromInventory;
        partner2SOData.OnCurrentHealthValueChanged -= UpdateHealthDisplayFromInventory;
        partner3SOData.OnCurrentHealthValueChanged -= UpdateHealthDisplayFromInventory;
        partner1SOData.OnStaminaValueChanged -= UpdateStaminaDisplayFromInventory;
        partner2SOData.OnStaminaValueChanged -= UpdateStaminaDisplayFromInventory;
        partner3SOData.OnStaminaValueChanged -= UpdateStaminaDisplayFromInventory;
        partner1SOData.onInjuredChanged -= UpdateConditionUIInjured;
        partner1SOData.onSickChanged -= UpdateConditionUISick;


    }

}
