using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable]
public class PlayerData
{    
    public float MaxHealth 
    {
        get {return maxHealth; }
        set {maxHealth = value;}
    }
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }
    public float Poise
    {
        get { return poise; }
        set { poise = value; }
    }
    public int SP
    {
        get { return sp; }
        set { sp = value; }
    }
    public int EP
    {
        get { return ep; }
        set { ep = value; }
    }
    public bool Sick
    {
        get { return sick; }
        set { sick = value; }
    }  
    public bool Injured
    {
        get { return injured; }
        set { injured = value; }
    }

    public int MaxEP
    {
        get { return maxEp; }
        set { maxEp = value; }
    }

    public int MaxSP
    {
        get { return maxSp; }
        set { maxSp = value; }
    }

    public float MaxStamina
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }

    public float MaxPoise
    {
        get { return maxPoise; }
        set { maxPoise = value; } 
    }


    //for now, these need to be public to be accessed by the saveGame function in game manager.
    //nvm, the serializefield attribute allow them to be accessed and stored. Thx ChatGPT!
    [SerializeField]
    float maxHealth = 5f;
    [SerializeField]
    float currentHealth = 3f;
    [SerializeField]
    float stamina = 10f;
    [SerializeField]
    float maxStamina = 10f;
    [SerializeField]
    float poise = 10f; 
    [SerializeField]
    float maxPoise = 10f;
    [SerializeField]
    int maxEp = 5;
    [SerializeField]
    int ep = 5;
    [SerializeField]
    int sp = 10;
    [SerializeField]
    int maxSp = 10;
    [SerializeField]
    bool injured;
    [SerializeField]
    bool sick;
}

[Serializable]
public class GameData
{
    public List<PlayerData> PlayerDatas = new List<PlayerData>();
    public string GameName;
}
