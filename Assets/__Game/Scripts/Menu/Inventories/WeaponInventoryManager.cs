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
    [SerializeField] GameObject playerWeaponInventoryContentPanel;
    [SerializeField] GameObject partnerWeaponInventoryContentPanel;
    [SerializeField] TextMeshProUGUI weaponDescriptionText;
    [SerializeField] GameObject equipButton;
    public WeaponInventoryItemSO currentWeapon;
    public event Action onPartnerWeaponSwapped;
    public event Action onPlayerWeaponSwapped;

    [SerializeField] Image playerPrimaryEquippedImage;
    [SerializeField] Image playerSecondaryEquippedImage;  
    [SerializeField] Image partnerPrimaryEquippedImage;
    [SerializeField] Image partnerSecondaryEquippedImage;
  //  PartnerWeaponState partnerWeaponStateInstance;

    [SerializeField] public List<WeaponInventoryItemSO> playerWeaponsInInventory = new List<WeaponInventoryItemSO>();
    [SerializeField] public List<WeaponInventoryItemSO> partnerWeaponsInInventory = new List<WeaponInventoryItemSO>();


    Image equipButtonImage;
    private void Awake()
    {
        PartnerWeaponState.Instance.SwitchPrimaryState(PrimaryWeaponState.MeleeBasic);
        PartnerWeaponState.Instance.SwitchSecondaryState(SecondaryWeaponState.BasicProjectile);
    }
    private void Start()
    {
        //partnerWeaponStateInstance = PartnerWeaponState.GetInstance();

        
                SetInitialPlayerPrimaryWeapon(playerWeaponsInInventory[0]); //bare hands
        SetInitialPlayerSecondaryWeapon(playerWeaponsInInventory[1]);   // bare hands projectile
        
                SetInitialPartnerPrimaryWeapon(partnerWeaponsInInventory[0]); //basic melee
        SetInitialPartnerSecondaryWeapon(partnerWeaponsInInventory[1]);  // basic projectile

      

    }


    void SetInitialPartnerPrimaryWeapon(WeaponInventoryItemSO partnerWeapon)
    {
      partnerPrimaryEquippedImage.sprite = partnerWeapon.weaponImage;
    }  void SetInitialPlayerPrimaryWeapon(WeaponInventoryItemSO playerWeapon)
    {
        playerPrimaryEquippedImage.sprite = playerWeapon.weaponImage;
    }
    void SetInitialPartnerSecondaryWeapon(WeaponInventoryItemSO partnerWeapon)
    {
        partnerSecondaryEquippedImage.sprite = partnerWeapon.weaponImage;
    }
    void SetInitialPlayerSecondaryWeapon(WeaponInventoryItemSO playerWeapon)
    {
        playerSecondaryEquippedImage.sprite = playerWeapon.weaponImage;
    }

    public void SetTextAndButton(string description, bool buttonActive)
    {
        if(equipButtonImage == null)
        {
            equipButtonImage = equipButton.GetComponent<Image>();
        }
        weaponDescriptionText.text = description;
        if (buttonActive && equipButtonImage)
        {
            equipButtonImage.color = Color.green;


        }
        else if (!buttonActive && equipButtonImage)
        {
            equipButtonImage.color = Color.red;

        }
        else return;
    }

     void MakeInventorySlots()
    {
        if (playerWeaponsInInventory.Count > 0)
        {
            for (int i = 0; i < playerWeaponsInInventory.Count; i++)
            {
                //condition for only if hasn't been made before
                if (i == 1)
                {
                    continue;
                }
                {
                    GameObject temp =
                        Instantiate(blankWeaponInventorySlot, playerWeaponInventoryContentPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(playerWeaponInventoryContentPanel.transform);

                    WeaponInventorySlot newSlot = temp.GetComponent<WeaponInventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerWeaponsInInventory[i], this);
                    }

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
                    Instantiate(blankWeaponInventorySlot, partnerWeaponInventoryContentPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(partnerWeaponInventoryContentPanel.transform);

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
        weaponDescriptionText.text = newDescription;
        SetTextAndButton(newDescription, isButtonActive);
    }

    private void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }
    private void OnDisable()
    {

        //sub to partnerWeapon global event then check this event in equip button pressed before running any logic
    }
    
    public void ClearAndMakeSlots()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    public void EquipButtonPressed()
    {//Potential need to create and call a mediator for handling the changing of weapon data. Use UI only here??
        if(currentWeapon)
        if (currentWeapon.isPlayerWeapon)
        {
            onPlayerWeaponSwapped.Invoke();
            SetEquippedImage(currentWeapon);
            SetTextAndButton("", false);
        }
        else
        {
            if (!PartnerWeapon.AnyInstanceAttacking())
            {
                if (currentWeapon.isPrimary)
                {
                    PartnerWeaponState.Instance.SwitchPrimaryState(currentWeapon.primaryType);
//                    partnerWeaponStateInstance.SwitchPrimaryState(currentWeapon.primaryType);
                }
                else
                {
                    PartnerWeaponState.Instance.SwitchSecondaryState(currentWeapon.secondaryType);
            //        partnerWeaponStateInstance.SwitchSecondaryState(currentWeapon.secondaryType);
                }
                onPartnerWeaponSwapped.Invoke();
                SetEquippedImage(currentWeapon);
                SetTextAndButton("", false);
            }
        }
        else
        {
            return;
        }
           

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
        for (int i = 0; i < partnerWeaponInventoryContentPanel.transform.childCount; i++)
        {
            Destroy(partnerWeaponInventoryContentPanel.transform.GetChild(i).gameObject);

        }  
        for (int i = 0; i < playerWeaponInventoryContentPanel.transform.childCount; i++)
        {
            Destroy(playerWeaponInventoryContentPanel.transform.GetChild(i).gameObject);

        }
    }

   

    // private Dictionary<string, weaponItem> weaponItems = new Dictionary<string, weaponItem>();
    public void AddToPlayerWeaponInventory(WeaponInventoryItemSO weapon)
    {
        if (weapon.isCableCordUpgrade && playerWeaponsInInventory[2] !=null)
        {
            playerWeaponsInInventory[2] = weapon;
            currentWeapon = weapon;
            EquipButtonPressed();
            
            ClearInventorySlots();
            MakeInventorySlots();

        }
        else
        {
            playerWeaponsInInventory.Add(weapon);
        }
        if(!weapon.isCableCordUpgrade)
        MakeNewInventorySlots(weapon);
    }
    public void AddToPartnerWeaponInventory(WeaponInventoryItemSO weapon)
    {
        partnerWeaponsInInventory.Add(weapon);
        MakeNewInventorySlots(weapon);

    }
   

    private void MakeNewInventorySlots(WeaponInventoryItemSO weapon)
    {
        if (weapon.isPlayerWeapon)
        {
            GameObject temp =
                            Instantiate(blankWeaponInventorySlot, playerWeaponInventoryContentPanel.transform.position, Quaternion.identity);
            temp.transform.SetParent(playerWeaponInventoryContentPanel.transform);

            WeaponInventorySlot newSlot = temp.GetComponent<WeaponInventorySlot>();
            if (newSlot)
            {
                newSlot.Setup(weapon, this);
            }
        }
        if (weapon.isPartnerWeapon)
        {
            GameObject temp =
                            Instantiate(blankWeaponInventorySlot, partnerWeaponInventoryContentPanel.transform.position, Quaternion.identity);
            temp.transform.SetParent(partnerWeaponInventoryContentPanel.transform);

            WeaponInventorySlot newSlot = temp.GetComponent<WeaponInventorySlot>();
            if (newSlot)
            {
                newSlot.Setup(weapon, this);
            }
        }

    }
   
}
