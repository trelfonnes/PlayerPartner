using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerSOData : ScriptableObject
{
    public float moveSpeed = 5;
    public float watchSpeed = 0;
    public float followSpeed = 5;
    
}
