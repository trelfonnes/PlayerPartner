using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class AttackTeleport : AttackData
{
    [field: SerializeField] public float minTeleportDistance { get; private set; }
    [field: SerializeField] public float maxTeleportDistance { get; private set; }


}
