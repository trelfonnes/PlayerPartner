using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class AttackPowerOn : AttackData
{
    [field: SerializeField] public float Amount { get; private set; }

}
