using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set;}
    [field: SerializeField] public int NumberOfAttacks { get; private set; }

    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }
 
    public List<Type> GetAllDependencies(int character)
    {
        if (character == 0)
        {
            return ComponentData.Select(component => component.PartnerComponentDependency).ToList();
        }
        if (character == 1)
        {
            return ComponentData.Select(component => component.PlayerComponentDependency).ToList();
        }
        else
        {
            Debug.LogError("Player or Partner componentDependency not set");
            return new List<Type>();

        }

    }

    public void AddData(ComponentData data)
    {
        if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
            return;


        ComponentData.Add(data);
    }


   

}
