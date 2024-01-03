using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "New BattleArenaData", menuName = "BattleArenaData")]
[Serializable]
public class BattleArenaDataSO : ScriptableObject
{
    public GameObject enemyPrefab;
    public EnemyType enemyType;
    public int enemiesToSpawn = 1;
    public PartnerType partnerType;
    public AreaType areaType;
    public bool hasBeenDefeated;




}
