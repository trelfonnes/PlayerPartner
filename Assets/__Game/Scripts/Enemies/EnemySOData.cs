using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Basic Data")]

[System.Serializable]
public class EnemySOData : ScriptableObject
{

    public float health;
    public float moveSpeed;


}
