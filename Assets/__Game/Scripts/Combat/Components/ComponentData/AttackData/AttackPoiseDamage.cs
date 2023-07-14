using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackPoiseDamage : AttackData
{
    [field: SerializeField] public float amount { get; private set; }

}
