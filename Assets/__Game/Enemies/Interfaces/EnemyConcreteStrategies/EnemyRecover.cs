using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecover : IEnemyLowHealth
{
    public void StartLowHealthStrategy(EnemySOData data, EnemyMovement movement, EnemyCollisionSenses collisionSenses, EnemyStats stats)
    {
        //play healing sound w particle effec access stats or data to recover

        float percentToHeal = data.maxHealth *= .6f;
        float roundedHeal = Mathf.RoundToInt(percentToHeal);
        stats.IncreaseHealth(roundedHeal);
    }
}
