using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent 
{
    public abstract bool CheckCondition(PlayerInventory inventory);
    public abstract void Trigger();
}

public class KeyItemEvent : GameEvent
{
    string keyItemName;
    System.Action action;

    public KeyItemEvent(string keyItemName, System.Action action)
    {
        this.keyItemName = keyItemName;
        this.action = action;
    }

    public override bool CheckCondition(PlayerInventory inventory)
    {
        return PlayerInventory.Instance.HasKeyItem(keyItemName);
    }

    public override void Trigger()
    {
        action?.Invoke();
    }
}
