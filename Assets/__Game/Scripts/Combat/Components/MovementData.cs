using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : ComponentData
{
    [field: SerializeField] public AttackMovementData[] AttackData { get; private set; }
    
}
