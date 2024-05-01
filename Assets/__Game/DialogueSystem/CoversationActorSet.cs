using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoversationActorSet : MonoBehaviour
{
    //This class works in coordination with say, Basic NPC so it can remain interactiable 
    //while still receiving dynamic information on who the actor is
    Transform actor;


    public Transform GetActor()
    {
        if (actor)
        {
            return actor;
        }
        else return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
                actor= collision.transform;
            }
    }
}
