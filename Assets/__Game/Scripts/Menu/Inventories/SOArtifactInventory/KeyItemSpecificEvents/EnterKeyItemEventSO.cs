using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnterKeyItemEvent", menuName = "Game Events/EnterKeyItemEvent")]

public class EnterKeyItemEventSO : ScriptableObject
{
    [SerializeField] string requiredKeyItemName;
    [SerializeField] string actionMessage;
    [SerializeField] GameObject ObjectToInfluence; //drag and drop gameObject into inspector here.
    PlayerArtifactInventory inventory;
    public string RequiredKeyItemName => requiredKeyItemName;
    public string ActionMessage => actionMessage;

    public bool CheckCondition(PlayerArtifactInventory inventory)
    {
        this.inventory = inventory;
        return inventory.HasKeyItem(requiredKeyItemName);
    }

    public void Trigger()
    {
        Debug.Log(actionMessage);
       // inventory.ClearInventory();
        // Implement any additional action to be triggered here, such as opening a door, activating a trigger, etc.
        // put logic for changing the objectToInfluence or any other thing that can be referenced and changed.

    
    }


}
