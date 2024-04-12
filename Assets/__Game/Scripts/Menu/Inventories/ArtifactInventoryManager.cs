using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    [SerializeField] GameObject FirstMenuButton; //Make sure this is serialized to desired inital button in INSPECTOR

    public void SetText(string description)
    {
        descriptionText.text = description;

    }
     void SetInitialMenuButton() //Need to set the selected button manually when Menu opens because event system can't track it itself.
    {
        if(FirstMenuButton != null)
        {
            EventSystem.current.firstSelectedGameObject = FirstMenuButton;
            EventSystem.current.SetSelectedGameObject(FirstMenuButton);
        }
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
        SetInitialMenuButton();
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
