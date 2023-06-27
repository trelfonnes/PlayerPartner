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
    // [4] lowHP or multiple at once
    protected override void Awake()
    {
        base.Awake();
        partner1SOData.isInjuredChanged.AddListener(UpdateConditionUIInjured);
        partner1SOData.isSickChanged.AddListener(UpdateConditionUISick);
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

}
