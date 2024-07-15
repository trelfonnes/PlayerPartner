using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursueComponent : WeaponComponent<PursueData, AttackPursue>
{
    EnemyMovement movement;
    EnemyCollisionSenses collisions;

    protected override void Start()
    {
        base.Start();
        movement = EnemyCore.GetCoreComponent<EnemyMovement>();
        collisions = EnemyCore.GetCoreComponent<EnemyCollisionSenses>();
        EnemyEventHandler.onPursuePlayer += PursuePlayer;
    }
    void PursuePlayer()
    {
        if (collisions.playerTransform && movement) 
        {

            movement.Pursue(collisions.playerTransform); 
               // movement.ChargePartner(4f, collisions.playerTransform);
            
        }
        else if (collisions.partnerTransform && movement)
        {

            movement.Pursue(collisions.playerTransform);

            // movement.ChargePartner(4f, collisions.partnerTransform);

        }
        else
        {
            Debug.LogWarning("From Trel: Movement is null or playerTransform is null.");
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EnemyEventHandler.onPursuePlayer -= PursuePlayer;
    }


}
