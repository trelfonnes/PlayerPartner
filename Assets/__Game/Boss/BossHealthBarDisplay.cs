using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarDisplay : MonoBehaviour
{
    [SerializeField] Image healthMeter;
    [SerializeField] EnemyStatEvents bossStatEvents;
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

     void TurnOffHealthBar()
    {
        healthMeter.transform.parent.gameObject.SetActive(false);
    }
    void TurnOnHealthBar()
    {
        healthMeter.transform.parent.gameObject.SetActive(true);

    }
    private void OnEnable()
    {
        bossStatEvents.onBattleStart += TurnOnHealthBar;
        bossStatEvents.onHealthZero += TurnOffHealthBar;
    }
    private void OnDisable()
    {
        bossStatEvents.onBattleStart -= TurnOnHealthBar;
        bossStatEvents.onHealthZero -= TurnOffHealthBar;

    }
}
