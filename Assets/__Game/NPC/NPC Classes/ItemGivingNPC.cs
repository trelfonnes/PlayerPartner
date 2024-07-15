using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGivingNPC : BasicNPC
{
    [SerializeField] GameObject itemToGive;
    public override void Interact()
    {
        //not sure yet.. but will need to work with dialogue conditionally too.
        if (!npcData.hasGivenItem)
        {
            SpawnItem();
            Debug.Log("Hey, have this item.");
            //give dialogue response and give item
        }
        else
        {
            Debug.Log("Hey, I already gave you an item.");

            //give dialogue response telling the player already given item.
        }

    }

    void SpawnItem()
    {
        Instantiate(itemToGive, (transform.position * -1.5f), Quaternion.identity);
        npcData.hasGivenItem = true;

    }

}
