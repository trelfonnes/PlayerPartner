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
    public int MP
    {
        get { return mp; }
        set { mp = value; }
    }
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    //for now, these need to be public to be accessed by the saveGame function in game manager.
    //nvm, the serializefield attribute allow them to be accessed and stored. Thx ChatGPT!
    [SerializeField]
    float maxHealth = 3f;
    [SerializeField]
    float currentHealth = 3f;
    [SerializeField]
    int mp = 10;
}

[Serializable]
public class GameData
{
    public List<PlayerData> PlayerDatas = new List<PlayerData>();
    public string GameName;
}
