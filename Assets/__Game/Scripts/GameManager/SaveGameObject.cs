using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SaveLoadManager.Instance.SaveGame();
        Debug.Log("Data Saved");
    }
}
