using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BattleArenaData", menuName = "BattleArenaData")]

public class BattleArenaDataSO : ScriptableObject
{
    public GameObject enemyPrefab;
    public EnemyType enemyType;
    public int enemiesToSpawn = 1;
    public PartnerType partnerType;
    public AreaType areaType;

   


}
