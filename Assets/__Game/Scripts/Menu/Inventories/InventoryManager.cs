using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Information")]
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] GameObject blankInventorySlot;
    [SerializeField] GameObject inventoryContentPanel;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] GameObject useButton;
    public inventoryItems currentItem;
    Image equipButtonImage;
    [SerializeField] GameObject FirstMenuButton;
    private void Start()
    {
       

    }
     void SetInitialMenuButton() //Need to set the selected button manually when Menu opens because event system can't track it itself.
    {
        if (FirstMenuButton != null)
        {
            EventSystem.current.firstSelectedGameObject = FirstMenuButton;
            EventSystem.current.SetSelectedGameObject(FirstMenuButton);
        }
    }
    public void SetTextAndButton(string description, bool buttonActive)
    {
        if(equipButtonImage == null)
        {
            equipButtonImage = useButton.GetComponent<Image>();
        }
        descriptionText.text = description;
        if (buttonActive)
        {
            equipButtonImage.color = Color.green;


        }
        else
        {
            equipButtonImage.color = Color.red;

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
        SetInitialMenuButton();
        
    }
 

    public void SetupDescriptionAndButton(string newDescription, bool isButtonActive, inventoryItems newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescription;
        SetTextAndButton(newDescription, isButtonActive);
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
        else
        {
            return;
        }
    }

}
