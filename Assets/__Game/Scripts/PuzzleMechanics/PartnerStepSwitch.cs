using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PartnerStepSwitch : MonoBehaviour
{
    [SerializeField] bool isLevelOne;
    [SerializeField] bool isLevelTwo;
    [SerializeField] bool isLevelThree;
    [SerializeField] Sprite switchUp;
    [SerializeField] Sprite switchDown;
    SpriteRenderer sr;
    bool switchPressed;
    public event Action<int> onPartnerStepSwitchActivated; 
    // any object can sub to switch being pressed and execute its functionality. i.e. chest drops door opens gate opens. 

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = switchUp;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Partner") && !collision.isTrigger)
        {
            if (isLevelOne)
            {
                switchPressed = true;
                ActivateSwitch(1);

            }
            Partner partner = collision.gameObject.GetComponent<Partner>();
            if (isLevelTwo)
            {
                if (!partner.stageOne)
                {
                    switchPressed = true;
                    ActivateSwitch(2);

                }
            }
            if (isLevelThree)
            {
                if (partner.stageThree)
                {
                    switchPressed = true;
                    ActivateSwitch(3);
                }
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner") && !collision.isTrigger)
        {
            sr.sprite = switchUp;
            switchPressed = false;
        }
    }

    void ActivateSwitch(int switchLevel)
    {
        if (switchPressed)
        {
                sr.sprite = switchDown;
            
            onPartnerStepSwitchActivated?.Invoke(switchLevel);
            //play clicking sound
        }
    }
}
