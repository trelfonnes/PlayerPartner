using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class AttackPursue : AttackData
{
    [field: SerializeField] public float pursueSpeed { get; private set; }
}
