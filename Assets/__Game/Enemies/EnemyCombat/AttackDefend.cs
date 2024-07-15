using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class AttackDefend : AttackData
{
  [field: SerializeField] public float defendDuration { get; private set; }
}
