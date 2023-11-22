using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

[System.Serializable]
public class PlayerSOData : BaseSOPlayerData
{
    [Header("Player and Partner Movement Speeds")]
    public float moveSpeed = 5;
    public float watchSpeed = 0;
    public float followSpeed = 5;

    [Header("Player and Partner SharedData")]
    public float MaxHealth = 5;
    //public float CurrentHealth = 5;
    public float HealthLimit;
    public int SP = 0;
    public int MaxSP = 25;
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;

    [Header("Partner exclusive Data")]
    //public float Stamina = 50;
    public float MaxStamina = 50;
    public float StaminaLimit;
    public float Poise;
    public float MaxPoise;
    public bool canJump;
    public int numberOfJumps = 1;
    public float jumpForce = 1f;
    public float jumpDistance = 1f;
    public bool canDash;
    public float dashTime;
    public int numberOfDashes = 1;
    public float dashForce = 3f;

    [Header("Stage Identifier")]
    public bool stage1 = false;
    public bool stage2 = false;
    public bool stage3 = false;
    public bool isPlayer = false;

    [Header("Player exclusive Data")]
    public int Bytes = 0;
    public int MaxBytes = 50;
    public int Keys;
    public int BossKeys;



}
