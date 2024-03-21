using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] GameObject blankInventorySlot;
    [SerializeField] GameObject inventoryContentPanel;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] GameObject useButton;
    public inventoryItems currentItem;
    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if (playerInventory.myInventory[i].numberHeld > 0)// || it is equal to "bottle" meaning mimicing zelda empty bottle
                {
                    GameObject temp =
                        Instantiate(blankInventorySlot, inventoryContentPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryContentPanel.transform);

                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.myInventory[i], this);
                    }
                }
                else
                {
                    // Remove items with numberHeld less than or equal to zero
                    playerInventory.myInventory.RemoveAt(i);
                    // Since we removed an item, decrement the loop index to properly iterate through the modified list
                    i--;
                }

            }

        }
    }

    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
        
    }


    public void SetupDescriptionAndButton(string newDescription, bool isButtonActive, inventoryItems newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescription;
        useButton.SetActive(isButtonActive);
    }

    void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryContentPanel.transform.childCount; i++)
        {
            Destroy(inventoryContentPanel.transform.GetChild(i).gameObject);

        }
    }
    public void UseButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
            ClearInventorySlots();
            MakeInventorySlots();
            SetTextAndButton("", false);
        }
    }

}
