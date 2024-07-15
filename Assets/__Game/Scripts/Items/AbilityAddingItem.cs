using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAddingItem : DataReferenceInheritor
{
    // only one of these items should exist in the game for each
    string nameOfItem;
   
    [SerializeField] bool isPowerGlove;
    [SerializeField] bool isDashAbility;
    [SerializeField] bool isJumpAbility;
    bool hasBeenCollected;
    private void Start()
    {
        if (isPowerGlove)
        {
            nameOfItem = "PowerGlove";
        }
        if (isDashAbility)
        {
            nameOfItem = "Dash";
        }
        if (isJumpAbility)
        {
            nameOfItem = "Jump";
        }
        if (ES3.KeyExists(nameOfItem))
        {
                
            hasBeenCollected = ES3.Load<bool>(nameOfItem);
            if (hasBeenCollected)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenCollected) 
        { 
         if (collision.CompareTag("Player") && !collision.isTrigger)
         {
            if (isPowerGlove)
            {
                playerSOData.carryHeavy = true;
                    TurnOffThisAbilityItem();
            }
         }
         if (collision.CompareTag("Partner") && !collision.isTrigger)
          {
            if (isDashAbility)
            {
                partner1SOData.canDash = true;
                partner2SOData.canDash = true;
                partner3SOData.canDash = true;
                    TurnOffThisAbilityItem();

                }
                if (isJumpAbility)
            {
                partner1SOData.canJump = true;
                partner2SOData.canJump = true;
                partner3SOData.canJump = true;
                    TurnOffThisAbilityItem();

                }
            }
        }

    }
    void TurnOffThisAbilityItem()
    {
        hasBeenCollected = true;
        ES3.Save(nameOfItem, hasBeenCollected);
        gameObject.SetActive(false);

    }
}
