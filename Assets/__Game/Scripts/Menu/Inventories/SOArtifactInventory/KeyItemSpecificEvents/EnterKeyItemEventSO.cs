using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnterKeyItemEvent", menuName = "Game Events/EnterKeyItemEvent")]

public class EnterKeyItemEventSO : ScriptableObject
{
    [SerializeField] string requiredKeyItemName;
    [SerializeField] string actionMessage;
    PlayerArtifactInventory inventory;
    public string RequiredKeyItemName => requiredKeyItemName;
    public string ActionMessage => actionMessage;

    public bool CheckCondition(PlayerArtifactInventory inventory)
    {
        this.inventory = inventory;
        return inventory.HasKeyItem(requiredKeyItemName);
    }

    


}
