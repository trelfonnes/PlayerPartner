using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveTypeStrategyFactory 
{
    public static IAttackTypeDamageCalculation CreateStrategy(DefensiveType defensiveType)
    {
        switch (defensiveType)
        {
            case DefensiveType.Fire:
                return new FireTypeStrategy();

            default:
                return new FireTypeStrategy(); //TODO: make default normalType strategy
        }
    }


}
