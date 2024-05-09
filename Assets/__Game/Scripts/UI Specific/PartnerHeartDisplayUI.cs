using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerHeartDisplayUI : HeartDisplayUI
{
    protected override void Awake()
    {
        base.Awake();
        partner1SOData.OnCurrentHealthValueChanged += UpdateDisplayViaInventory;
        partner2SOData.OnCurrentHealthValueChanged += UpdateDisplayViaInventory;
        partner3SOData.OnCurrentHealthValueChanged += UpdateDisplayViaInventory;
    }

    private void UpdateDisplayViaInventory(float currentHealth)
    {
        
            int fullHeartsCount = Mathf.CeilToInt(currentHealth);

            for (int i = 0; i < heartsFull.Count; i++)
            {
                if (i < fullHeartsCount)
                {
                    heartsFull[i].gameObject.SetActive(true);
                }
                else
                {
                    heartsFull[i].gameObject.SetActive(false);
                }

            }
       
    }

    private void OnDisable()
    {
        partner1SOData.OnCurrentHealthValueChanged -= UpdateDisplayViaInventory;
        partner2SOData.OnCurrentHealthValueChanged -= UpdateDisplayViaInventory;
        partner3SOData.OnCurrentHealthValueChanged -= UpdateDisplayViaInventory;

    }
}
