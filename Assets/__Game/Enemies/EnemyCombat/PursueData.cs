using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueData : ComponentData<AttackPursue>
{
  protected override void SetComponentDependency()
    {
        EnemyComponentDependency = typeof(EnemyPursueComponent);
    }


}
