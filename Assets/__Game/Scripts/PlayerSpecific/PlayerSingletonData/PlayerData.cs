using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable]
public class PlayerData 
{
        public int health = 3;
        public int mp = 10;
}

[Serializable]
public class GameData
{
    public List<PlayerData> PlayerDatas = new List<PlayerData>();
}
