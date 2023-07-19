using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class AttackActionHItBox : AttackData
{
    public bool DebugHitBoxNorth;
    [field: SerializeField] public Rect HitBoxNorth { get; private set; }
    public bool DebugHitBoxSouth;

    [field: SerializeField] public Rect HitBoxSouth { get; private set; }
    public bool DebugHitBoxEast;

    [field: SerializeField] public Rect HitBoxEast { get; private set; }
    public bool DebugHitBoxWest;

    [field: SerializeField] public Rect HitBoxWest { get; private set; }
    public bool DebugHitBoxNorthWest;

    [field: SerializeField] public Rect HitBoxNorthWest { get; private set; }
    public bool DebugHitBoxSouthWest;

    [field: SerializeField] public Rect HitBoxSouthWest { get; private set; }
    public bool DebugHitBoxNorthEast;

    [field: SerializeField] public Rect HitBoxNorthEast { get; private set; }
    public bool DebugHitBoxSouthEast;

    [field: SerializeField] public Rect HitBoxSouthEast { get; private set; }
}
