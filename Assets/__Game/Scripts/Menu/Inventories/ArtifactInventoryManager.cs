using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class ArtifactInventoryManager : MonoBehaviour
{
    [Header("Artifact Inventory Information")]
    [SerializeField] PlayerArtifactInventory artifactInventory;
    [SerializeField] GameObject blankInventorySlotPrefab;
    [SerializeField] GameObject inventoryContentPanel;
    [SerializeField] TextMeshProUGUI descriptionText; // the 
    [SerializeField] Image artifactImage;  //the blown up image UI element
    public ArtifactInventoryItems currentItem; 


    public void SetText(string description)
    {
        descriptionText.text = description;

    }

    void MakeInventorySlots()
    {
        if (artifactInventory)
        {
            for (int i = 0; i < artifactInventory.artifactInventory.Count; i++)
            {
                GameObject temp =
                    Instantiate(blankInventorySlotPrefab, inventoryContentPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryContentPanel.transform);

                ArtifactInventorySlot newSlot = temp.GetComponent<ArtifactInventorySlot>();
                if (newSlot)
                {
                    newSlot.Setup(artifactInventory.artifactInventory[i], this);
                }
            }
        }
    }
    public void AddArtifactToInventory(ArtifactInventoryItems artifact)
    {
        artifactInventory.artifactInventory.Add(artifact);
    }

    private void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetText("");
    }
    private void OnDisable()
    {
        this.artifactImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    public void SetupDescriptionAndImage(string newDescription, Sprite artifactImage, ArtifactInventoryItems newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescription;
        this.artifactImage.sprite = artifactImage;
        this.artifactImage.gameObject.SetActive(true);
    }

   void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryContentPanel.transform.childCount; i++)
        {
            Destroy(inventoryContentPanel.transform.GetChild(i).gameObject);
        }
    }
}
