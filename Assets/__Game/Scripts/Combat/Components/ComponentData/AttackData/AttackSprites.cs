using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackSprites: AttackData
{
    [field: SerializeField] public PhaseSprites[] PhaseSprites { get; private set; }
}
[Serializable]
public struct PhaseSprites
{
    [field: SerializeField] public AttackPhases Phase { get; private set; }
    [field: SerializeField] public AttackPhases PhaseDirection { get; private set; }
    [field: SerializeField] public Sprite[] Sprites { get; private set; }

}

