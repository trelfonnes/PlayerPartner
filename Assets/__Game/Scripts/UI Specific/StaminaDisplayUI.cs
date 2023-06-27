using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplayUI : MonoBehaviour
{
    [SerializeField] Image staminaMeter;


    public void UpdateStaminaDisplay(float currentStamina, float maxStamina)
    {
        float fillAmount = currentStamina / maxStamina;
        if (staminaMeter != null)
        {
            staminaMeter.fillAmount = fillAmount;
        }
    }

}
