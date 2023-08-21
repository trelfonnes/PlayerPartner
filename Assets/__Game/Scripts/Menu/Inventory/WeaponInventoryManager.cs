using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public enum PartnerStage
{
    stage1,
    stage2,
    stage3
}
public class WeaponInventoryManager : MonoBehaviour
{
   


    [Header("Weapon Inventory Information")]
    [SerializeField] GameObject blankWeaponInventorySlot;
    [SerializeField] GameObject weaponInventoryContentPanel;
    [SerializeField] TextMeshProUGUI weaponDescriptionText;
    [SerializeField] GameObject equipButton;
    public WeaponInventoryItemSO currentWeapon;
    public event Action onPartnerWeaponSwapped;
    public event Action onPlayerWeaponSwapped;

    [SerializeField] Image playerPrimaryEquippedImage;
    [SerializeField] Image playerSecondaryEquippedImage;  
    [SerializeField] Image partnerPrimaryEquippedImage;
    [SerializeField] Image partnerSecondaryEquippedImage;
    PartnerWeaponState partnerWeaponStateInstance;

    [SerializeField] List<WeaponInventoryItemSO> playerWeaponsInInventory = new List<WeaponInventoryItemSO>();
    [SerializeField] List<WeaponInventoryItemSO> partnerWeaponsInInventory = new List<WeaponInventoryItemSO>();


    private void Start()
    {
        partnerWeaponStateInstance = PartnerWeaponState.GetInstance();

        
                SetInitialPlayerWeapon(playerWeaponsInInventory[0]);
           
        
                SetInitialPartnerWeapon(partnerWeaponsInInventory[0]);
            
    }
   

    void SetInitialPartnerWeapon(WeaponInventoryItemSO partnerWeapon)
    {
      partnerPrimaryEquippedImage.sprite = partnerWeapon.weaponImage;
    }  void SetInitialPlayerWeapon(WeaponInventoryItemSO playerWeapon)
    {
        playerPrimaryEquippedImage.sprite = playerWeapon.weaponImage;
    }
    public void SetTextAndButton(string description, bool buttonActive)
    {
        weaponDescriptionText.text = description;
        if (buttonActive)
        {
            equipButton.SetActive(true);
        }
        else
        {
            equipButton.SetActive(false);
        }
    }

    void MakeInventorySlots()
    {
        if (playerWeaponsInInventory.Count > 0)
        {
            for (int i = 0; i < playerWeaponsInInventory.Count; i++)
            {
                //condition for only if hasn't been made before
                    GameObject temp =
                        Instantiate(blankWeaponInventorySlot, weaponInventoryContentPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(weaponInventoryContentPanel.transform);

                    WeaponInventorySlot newSlot = temp.GetComponent<WeaponInventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerWeaponsInInventory[i], this);
                    }
                
            }

        }
        //setting the initial "Melee" attack image. When this slot is equipped, set a flag, other stages check that flag when set to active. 
        //only using stage 1 weapon list and 2 and 3 will always correspond. on enable event of stage 2 and 3, allweaponobjectreference will subscribe and set to what the flag tells.
        if (partnerWeaponsInInventory.Count > 0)
        {
            for (int i = 0; i < partnerWeaponsInInventory.Count; i++)
            {
                //condition for only if hasn't been made before
                GameObject temp =
                    Instantiate(blankWeaponInventorySlot, weaponInventoryContentPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(weaponInventoryContentPanel.transform);

                WeaponInventorySlot newSlot = temp.GetComponent<WeaponInventorySlot>();
                if (newSlot)
                {
                    newSlot.Setup(partnerWeaponsInInventory[i], this);
                }

            }

        }


    }
    public void SetupDescriptionAndButton(string newDescription, bool isButtonActive, WeaponInventoryItemSO newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("set description for weapon");
        weaponDescriptionText.text = newDescription;
        equipButton.SetActive(isButtonActive);
    }

    private void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    public void EquipButtonPressed()
    {//Potential need to create and call a mediator for handling the changing of weapon data. Use UI only here??

        if (currentWeapon.isPlayerWeapon)
        {
            onPlayerWeaponSwapped.Invoke();

        }
        else
        {
            if (currentWeapon.isPrimary)
            {
                partnerWeaponStateInstance.SwitchPrimaryState(currentWeapon.primaryType);
            }
            else
            {
                partnerWeaponStateInstance.SwitchSecondaryState(currentWeapon.secondaryType);
            }
            onPartnerWeaponSwapped.Invoke();
        }
            SetEquippedImage(currentWeapon);
        SetTextAndButton("", false);

    }

    void SetEquippedImage(WeaponInventoryItemSO currentWeapon)
    {
        // TODO: add place to initialize initial equipped weapon images.
        if (currentWeapon.isPlayerWeapon)
        {
            if (currentWeapon.isPrimary)
            {
                playerPrimaryEquippedImage.sprite = currentWeapon.weaponImage;
            }

            else
            {
                playerSecondaryEquippedImage.sprite = currentWeapon.weaponImage;
            }
        }
        else
        {
            if (currentWeapon.isPrimary)
            {
                partnerPrimaryEquippedImage.sprite = currentWeapon.weaponImage;
            }
            else
            {
                partnerSecondaryEquippedImage.sprite = currentWeapon.weaponImage;
            }
        }
    }

    void ClearInventorySlots()
    {
        for (int i = 0; i < weaponInventoryContentPanel.transform.childCount; i++)
        {
            Destroy(weaponInventoryContentPanel.transform.GetChild(i).gameObject);

        }
    }

   

    // private Dictionary<string, weaponItem> weaponItems = new Dictionary<string, weaponItem>();
    public void AddToPlayerWeaponInventory(WeaponInventoryItemSO weapon)
    {
        playerWeaponsInInventory.Add(weapon);
        MakeNewInventorySlots(weapon);

    }
    public void AddToPartnerWeaponInventory(WeaponInventoryItemSO weapon)
    {
        partnerWeaponsInInventory.Add(weapon);
        MakeNewInventorySlots(weapon);

    }
   

    private void MakeNewInventorySlots(WeaponInventoryItemSO weapon)
    {
        GameObject temp =
                        Instantiate(blankWeaponInventorySlot, weaponInventoryContentPanel.transform.position, Quaternion.identity);
        temp.transform.SetParent(weaponInventoryContentPanel.transform);

        WeaponInventorySlot newSlot = temp.GetComponent<WeaponInventorySlot>();
        if (newSlot)
        {
            newSlot.Setup(weapon, this);
        }
    }
}
