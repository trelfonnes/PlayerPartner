using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newNPCData", menuName = "Data/NPC Data")]

public class NPCDataSO : ScriptableObject
{

    public string npcID;
    public bool hasGivenItem;
    public bool hasReceivedItem;
    public bool hasSpokenToPlayer;
   


}
