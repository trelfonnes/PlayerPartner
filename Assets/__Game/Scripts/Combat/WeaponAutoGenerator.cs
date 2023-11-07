using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WeaponAutoGenerator : MonoBehaviour
{
    // this class interacts with other classes to change out weapons.
    [SerializeField] Weapon weapon;
    [SerializeField] PartnerWeapon partnerWeapon;  //drag references in inspector
    
    [SerializeField] WeaponDataSO data; // drag data desired for weapon here for now
                                        // eventually it will be passed by inventory
    private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
     private List<WeaponComponent> componentAddedToWeapon = new List<WeaponComponent>();
    private List<Type> componentDependencies = new List<Type>();
    int character; // used to differentiate player or partner components should be added
    Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        
            GenerateWeapon(data);
        
    }

    [ContextMenu("Test Generate")]
    private void TestGeneration()
    {
        GenerateWeapon(data);
    }

    public void GenerateWeapon(WeaponDataSO data) //eventually called from inventory or whatever system to generate the weapon
    {
        if (weapon != null)
        {
            weapon.SetData(data);
            character = 1;
        }
        if (partnerWeapon != null)
        {
            partnerWeapon.SetData(data);
            character = 0;
        }
        else//forEnemies. Don't pass data here because no inventory for them
        {
           character = 2;
        }
        componentAlreadyOnWeapon.Clear();
        componentAddedToWeapon.Clear();
        componentDependencies.Clear();

        componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

        componentDependencies = data.GetAllDependencies(character);

        foreach (var dependency in componentDependencies) // check if same type item in list has been added, if so go to next
        {
            if (componentAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                continue;

            var weaponComponent = componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

            if (weaponComponent == null)
            {

                //add to list that its been added
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent; // cast here

            }

            weaponComponent.Init();

            componentAddedToWeapon.Add(weaponComponent); //finally add the weapon to the list!
        }

        //remove ones no longer needed
        var componentsToRemove = componentAlreadyOnWeapon.Except(componentAddedToWeapon);// removes any item that is already on the other list
        // loop through this list and destroy each of those components
        foreach (var weaponComponent in componentsToRemove)
        {
            Destroy(weaponComponent);
        }
        if (anim != null && anim.runtimeAnimatorController != null)
        {
            anim.runtimeAnimatorController = data.AnimatorController;
        }
    }


}
