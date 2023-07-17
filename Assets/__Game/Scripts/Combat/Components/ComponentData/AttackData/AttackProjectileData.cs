using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackProjectileData : AttackData
{
    [field: SerializeField] public ProjectileType TypeOfProjectile { get; private set; }
}
