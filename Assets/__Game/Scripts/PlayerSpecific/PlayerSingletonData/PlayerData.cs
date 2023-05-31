using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable]
public class PlayerData 
{
    public float maxHealth = 3;
    public int mp = 10;
    public float currentHealth;
}

[Serializable]
public class GameData
{
    public List<PlayerData> PlayerDatas = new List<PlayerData>();
    public string GameName;
}
