using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class AttackMovementData: AttackData 
{
    
    [field: SerializeField] public Vector2 Direction { get; private set; }
    [field:SerializeField] public float Velocity { get; private set; }


}
