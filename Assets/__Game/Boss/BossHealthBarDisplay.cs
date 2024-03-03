using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarDisplay : MonoBehaviour
{
    [SerializeField] Image healthMeter;
    float MaxHealth = 30;
   
    public void UpdateDisplay(float currentHealth, float maxHealth)
    {
        if(healthMeter != null)
        {
            MaxHealth = maxHealth;
            float fillAmount = currentHealth / maxHealth;
            healthMeter.fillAmount = fillAmount;

        }
    }




}
