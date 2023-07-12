using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackKnockBack : AttackData 
{
    //direction knockback is applied in. Going to need to adapt for my topDown 2D
    [field: SerializeField] public Vector2 Angle { get; private set; }
    [field: SerializeField] public float Strength { get; private set; }


}
