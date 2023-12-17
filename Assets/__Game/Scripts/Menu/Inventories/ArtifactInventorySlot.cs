using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArtifactInventorySlot : MonoBehaviour
{
    [Header("Variables from this weapon")]
    private ArtifactInventoryItems thisItem;
    private ArtifactInventoryManager thisManager;

    [Header("UI Attributes to Change")]
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemName;
    public void Setup(ArtifactInventoryItems newItem, ArtifactInventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.artifactImage;
            itemName.text = thisItem.artifactName;
        }
        else
        {
            Debug.LogError("no artifact image");
        }

    }

    public void ClickedOn()
    {
        if (thisItem)
        {
            thisManager.SetupDescriptionAndImage(thisItem.artifactDescription, thisItem.largeDescriptionImage, thisItem);
        }
    }


}
