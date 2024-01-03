using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemiesInArea", menuName = "Data/EnemiesInArea Data/EnemiesInArea Data")]

public class EnemiesInAreaSO : ScriptableObject
{
    public List<EnemyType> commonAreaEnemies; 
    public List<EnemyType> uncommonAreaEnemies; 
    public List<EnemyType> rareAreaEnemies; 



}
