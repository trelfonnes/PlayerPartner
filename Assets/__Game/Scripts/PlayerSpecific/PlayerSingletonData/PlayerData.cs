using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable]
public class PlayerData: MonoBehaviour 
{
        public int health;
        public int mp;
}

[Serializable]
public class GameData
{
    public List<PlayerData> PlayerDatas = new List<PlayerData>();
}
