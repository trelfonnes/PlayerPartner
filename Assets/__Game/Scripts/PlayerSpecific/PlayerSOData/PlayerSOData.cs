using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerSOData : ScriptableObject
{
    [Header("Player and Partner Movement Speeds")]
    public float moveSpeed = 5;
    public float watchSpeed = 0;
    public float followSpeed = 5;

    [Header("Player and Partner SharedData")]
    public float MaxHealth = 5;
    public float CurrentHealth = 5;
    public int SP = 0;
    public int MaxSP = 25;

    [Header("Partner exclusive Data")]
    public float Stamina = 50;
    public float MaxStamina = 50;
    public float Poise;
    public float MaxPoise;
    public float EP = 0;
    public float MaxEP = 25;
    public bool Injured = false;
    public bool Sick = false;
    public bool stage2 = false;
    public bool stage3 = false;

    [Header("Player exclusive Data")]
    public int Bytes = 0;
    public int MaxBytes = 50;
}
