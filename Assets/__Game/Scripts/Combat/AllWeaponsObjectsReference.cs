using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllWeaponsObjectsReference : MonoBehaviour
{
    [SerializeField] GameObject primaryWeaponMale;
    [SerializeField] GameObject secondaryWeaponMale;

    [SerializeField] GameObject primaryWeaponFemale;
    [SerializeField] GameObject secondaryWeaponFemale;

    [SerializeField] GameObject primaryWeaponDino1;
    [SerializeField] GameObject secondaryWeaponDino1;

    [SerializeField] GameObject primaryWeaponDino2;
    [SerializeField] GameObject secondaryWeaponDino2;

    [SerializeField] GameObject primaryWeaponDino3;
    [SerializeField] GameObject secondaryWeaponDino3;

    [SerializeField] GameObject primaryWeaponRabbit1;
    [SerializeField] GameObject secondaryWeaponRabbit1;

    [SerializeField] GameObject primaryWeaponRabbit2;
    [SerializeField] GameObject secondaryWeaponRabbit2;

    [SerializeField] GameObject primaryWeaponRabbit3;
    [SerializeField] GameObject secondaryWeaponRabbit3;

    [SerializeField] GameObject primaryWeaponBear1;
    [SerializeField] GameObject secondaryWeaponBear1;

    [SerializeField] GameObject primaryWeaponBear2;
    [SerializeField] GameObject secondaryWeaponBear2;

    [SerializeField] GameObject primaryWeaponBear3;
    [SerializeField] GameObject secondaryWeaponBear3;

    [SerializeField] GameObject primaryWeaponAxel1;
    [SerializeField] GameObject secondaryWeaponAxel1;

    [SerializeField] GameObject primaryWeaponAxel2;
    [SerializeField] GameObject secondaryWeaponAxel2;

    [SerializeField] GameObject primaryWeaponAxel3;
    [SerializeField] GameObject secondaryWeaponAxel3;


    [SerializeField] GameObject primaryWeaponPartner1;
    [SerializeField] GameObject secondaryWeaponPartner1;
    [SerializeField] GameObject prmaryWeaponPartner2;
    [SerializeField] GameObject secondaryWeaponPartner2;
    [SerializeField] GameObject prmaryWeaponPartner3;
    [SerializeField] GameObject secondaryWeaponPartner3;

    [SerializeField] GameObject primaryWeaponPlayer;
    [SerializeField] GameObject secondaryWeaponPlayer;

    [SerializeField] WeaponAutoGenerator playerPrimaryGenerator;
    [SerializeField] WeaponAutoGenerator playerSecondaryGenerator;
    [SerializeField] WeaponAutoGenerator Partner1PrimaryGenerator;
    [SerializeField] WeaponAutoGenerator Partner2PrimaryGenerator;
    [SerializeField] WeaponAutoGenerator Partner3PrimaryGenerator;
    [SerializeField] WeaponAutoGenerator Partner1SecondaryGenerator;
    [SerializeField] WeaponAutoGenerator Partner2SecondaryGenerator;
    [SerializeField] WeaponAutoGenerator Partner3SecondaryGenerator;
   

    [SerializeField] bool male;
    [SerializeField] bool female;
    [SerializeField] bool dino;

    private void Awake() //TODO eventually refactor this to a function that is called by whatever class will determine which data and gameobjects are set.
    {
        if (dino)
        {
            primaryWeaponPartner1 = primaryWeaponDino1;
            secondaryWeaponPartner1 = secondaryWeaponDino1;
            prmaryWeaponPartner2 = primaryWeaponDino2;
            secondaryWeaponPartner2 = secondaryWeaponDino2;
            prmaryWeaponPartner3 = primaryWeaponDino3;
            secondaryWeaponPartner3 = secondaryWeaponDino3;
        }
        if (male)
        {
            primaryWeaponPlayer = primaryWeaponMale;
            secondaryWeaponPlayer = secondaryWeaponMale;
        }
        if (female)
        {
            primaryWeaponPlayer = primaryWeaponFemale;
            secondaryWeaponPlayer = secondaryWeaponFemale;
        }
        //add for each creature option
    }
    private void Start()
    {
        playerPrimaryGenerator = primaryWeaponPlayer.GetComponent<WeaponAutoGenerator>();
        playerSecondaryGenerator = secondaryWeaponPlayer.GetComponent<WeaponAutoGenerator>();
        Partner1PrimaryGenerator = primaryWeaponPartner1.GetComponent<WeaponAutoGenerator>();
        Partner2PrimaryGenerator = prmaryWeaponPartner2.GetComponent<WeaponAutoGenerator>();
        Partner3PrimaryGenerator = prmaryWeaponPartner3.GetComponent<WeaponAutoGenerator>();
        Partner1SecondaryGenerator = secondaryWeaponPartner1.GetComponent<WeaponAutoGenerator>();
        Partner2SecondaryGenerator = secondaryWeaponPartner2.GetComponent<WeaponAutoGenerator>();
        Partner3SecondaryGenerator = secondaryWeaponPartner3.GetComponent<WeaponAutoGenerator>();
    
    }


    public void EquipWeapon(WeaponDataSO currentWeapon)
    {
        if (currentWeapon.isPlayerWeapon)
        { 
            if (currentWeapon.isPrimary)
            {
                playerPrimaryGenerator.GenerateWeapon(currentWeapon);
            }
            else
            {
                playerSecondaryGenerator.GenerateWeapon(currentWeapon);
            } 
        }

        if (currentWeapon.isPartnerWeapon)
            {
                if (currentWeapon.isPrimary)
                {
                Partner1PrimaryGenerator.GenerateWeapon(currentWeapon);
                Partner2PrimaryGenerator.GenerateWeapon(currentWeapon);
                Partner3PrimaryGenerator.GenerateWeapon(currentWeapon);
                }
                else
                {
                Partner1SecondaryGenerator.GenerateWeapon(currentWeapon);
                Partner2SecondaryGenerator.GenerateWeapon(currentWeapon);
                Partner3SecondaryGenerator.GenerateWeapon(currentWeapon);

                }
            }
    }
}
