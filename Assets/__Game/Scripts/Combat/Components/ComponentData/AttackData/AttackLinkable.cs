using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class AttackLinkable : AttackData
{
    [field: SerializeField] public float Amount { get; private set; }

}