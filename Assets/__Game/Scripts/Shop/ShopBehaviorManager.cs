using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopBehaviorManager : MonoBehaviour, IInteractable
{
    //TODO: check for adequate currency amount. Spawn the item prefab at a spawn position
    [Header("ShopInventory Elements")]
    [SerializeField] ShopInventory shopInventory;
    [SerializeField] GameObject shopItemSlotPrefab;
    [SerializeField] GameObject shopInventoryContentPanel;
    [SerializeField] TextMeshProUGUI confirmPurchaseText;
    [SerializeField] Transform itemSpawnPoint;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject cancelFirstButton;
    [SerializeField] GameObject yesToConfirmButton;
    [SerializeField] GameObject cancelSecondButton;
    [SerializeField] GameObject notEnoughBytesMessage;
    public ShopItemSO currentItemToPurchase;
    [SerializeField] Collider2D playerCollider;

    public void SetTextAndButton(string description, bool buttonActive) 
    {
        descriptionText.text = description; // comes from ShopItem
        if (buttonActive)
        {
            buyButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(false);
        }
    }
    public void SetupDescriptionAndButton(string newDescription, bool isButtonActive, ShopItemSO newShopItem)
    {
        currentItemToPurchase = newShopItem;
        descriptionText.text = newDescription;
        buyButton.SetActive(isButtonActive);
        cancelFirstButton.SetActive(isButtonActive);
    }
    void MakeShopItemSlots()
    {
        if (shopInventory)
        {

            for (int i = 0; i < shopInventory.itemsInInventory.Count; i++)
            {
                if(shopInventory.itemsInInventory[i].amountForSale > 0)
                {
                    GameObject temp = Instantiate(shopItemSlotPrefab, shopInventoryContentPanel.transform.position, Quaternion.identity);

                    temp.transform.SetParent(shopInventoryContentPanel.transform);

                    ShopItem newSlot = temp.GetComponent<ShopItem>();
                    if (newSlot)
                    {
                        newSlot.SetupShopItem(this, shopInventory.itemsInInventory[i]);
                    }
                }
            }

        }


    }
    void OnEnable()
    {
        ClearShopItemSlots();
        MakeShopItemSlots();
        SetTextAndButton("", false);

    }

    void ClearShopItemSlots()
    {
        for(int i = 0; i < shopInventoryContentPanel.transform.childCount; i++)
        {
            Destroy(shopInventoryContentPanel.transform.GetChild(i).gameObject);
        }
    }
    

    public void BuyConfirmedButtonPressed() 
    {
        if (currentItemToPurchase.singlePurchaseItem)
        {
            ClearShopItemSlots();// only clear if its a single purchase item like a keyItem;
            MakeShopItemSlots();
        }
        SetTextAndButton("", false);

        PurchaseItem();
        
    }

 

    void PurchaseItem()
    {
        if (playerCollider)
        {
            int currentBytes = playerCollider.GetComponentInChildren<IBytes>().GetBytesAmount();
            if (currentBytes >= currentItemToPurchase.itemPrice)
            {
                playerCollider.GetComponentInChildren<IBytes>().DecreaseBytes(currentItemToPurchase.itemPrice);
                SpawnPurchasedItem();
            }
            else 
            {
               DisplayNotEnoughBytesMessage();
            }
        }


    }
    void DisplayNotEnoughBytesMessage()
    {
        notEnoughBytesMessage.SetActive(true);
        StartCoroutine(HideNotEnoughBytesMessageAfterDelay(2.0f)); // 2 seconds delay
    }

    private IEnumerator HideNotEnoughBytesMessageAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        notEnoughBytesMessage.SetActive(false);
    }
    void SpawnPurchasedItem()
    {
        // purched item prefab .transform.position = itemSpawnPoint.position;
    }
   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
             playerCollider = collision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollider = null;
        }
    }



    public void SetConfirmPurchaseText()
    {
        confirmPurchaseText.text = currentItemToPurchase.confirmPurchaseText;        // 
    }





    [SerializeField] GameObject ShopMenu;
    public void Interact()
    {
        OpenShopMenu();
    }

    private void OpenShopMenu()
    {
        ShopMenu.SetActive(true);
        ClearShopItemSlots();
        MakeShopItemSlots();
        SetTextAndButton("", false);
    }
}
