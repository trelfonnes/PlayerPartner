using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WeaponInventoryManager : MonoBehaviour
{
    [Header("references to player and partner weaponGameObjects")]
    [SerializeField] Weapon playerPrimaryWeapon;
    [SerializeField] Weapon playerSecondaryWeapon;
    [SerializeField] PartnerWeapon partnerPrimaryWeapon;
    [SerializeField] PartnerWeapon partnerSecondaryWeapon;


    [Header("Weapon Inventory Information")]
    [SerializeField] GameObject blankWeaponInventorySlot;
    [SerializeField] GameObject weaponInventoryContentPanel;
    [SerializeField] TextMeshProUGUI weaponDescriptionText;
    [SerializeField] GameObject equipButton;
    public WeaponDataSO currentWeapon;
    public List<WeaponDataSO> weaponDatas = new List<WeaponDataSO>();




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
        if (weaponDatas.Count > 0)
        {
            for (int i = 0; i < weaponDatas.Count; i++)
            {
                //condition for only if hasn't been made before
                    GameObject temp =
                        Instantiate(blankWeaponInventorySlot, weaponInventoryContentPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(weaponInventoryContentPanel.transform);

                    WeaponInventorySlot newSlot = temp.GetComponent<WeaponInventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(weaponDatas[i], this);
                    }
                
            }

        }
    }
    public void SetupDescriptionAndButton(string newDescription, bool isButtonActive, WeaponDataSO newWeapon)
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
            //move this slot to "equipped" space
            if (currentWeapon.isPrimary)
            {
                //TODO change picture in primary equipped slot
                playerPrimaryWeapon.Data = currentWeapon;
            }
            else
            {
                //TODO change picture in secondary equipped slot
                playerSecondaryWeapon.Data = currentWeapon;
            }
        }
        if (currentWeapon.isPartnerWeapon)
        {
            if (currentWeapon.isPrimary)
            {
                partnerPrimaryWeapon.Data = currentWeapon;
            }
            else
            {
                partnerSecondaryWeapon.Data = currentWeapon;
            }

        }
        SetTextAndButton("", false);

    }
    void ClearInventorySlots()
    {
        for (int i = 0; i < weaponInventoryContentPanel.transform.childCount; i++)
        {
            Destroy(weaponInventoryContentPanel.transform.GetChild(i).gameObject);

        }
    }

   

    // private Dictionary<string, weaponItem> weaponItems = new Dictionary<string, weaponItem>();
    public void AddToWeaponInventory(WeaponDataSO weapon)
    {
        weaponDatas.Add(weapon);
        MakeNewInventorySlots(weapon);

    }

    private void MakeNewInventorySlots(WeaponDataSO weapon)
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
