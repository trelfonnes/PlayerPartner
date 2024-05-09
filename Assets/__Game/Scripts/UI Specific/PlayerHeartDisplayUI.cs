using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartDisplayUI : HeartDisplayUI
{
    protected override void Awake()
    {
        base.Awake();
        playerSOData.OnCurrentPlayerHealthValueChanged += UpdatePlayerHeartDisplayViaInventory;

    }

    void UpdatePlayerHeartDisplayViaInventory(float currentHealth)
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
        playerSOData.OnCurrentPlayerHealthValueChanged -= UpdatePlayerHeartDisplayViaInventory;

    }
}
