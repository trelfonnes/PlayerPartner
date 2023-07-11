using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.Callbacks;
using System.Linq;
[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataSOEditor : Editor
{
    //reflection example!!
    static List<Type> dataCompTypes = new List<Type>(); //static makes so only one of these lists exists
    WeaponDataSO dataSO;

    bool showForceUpdateButtons;
    bool showAddComponentButtons;

    private void OnEnable()
    {
                //casting the target object into the type we need. target is built in editor
        dataSO = target as WeaponDataSO;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Set Number of Attacks"))
        {
            foreach (var item in dataSO.ComponentData)
            {
                item.InitializeAttackData(dataSO.NumberOfAttacks); //the button that makes the attack amount array equal to number of attacks
            }
        }

        showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");// foldout (dropDownArrow) that opens contents "buttons" when the bool is true

        if (showAddComponentButtons)
        {
            foreach (var dataCompType in dataCompTypes)
            {
                if (GUILayout.Button(dataCompType.Name))
                {//component data we want and instantiating a specific instance of that type. Activator to store info in a type class and create an object that has that type
                    var comp = Activator.CreateInstance(dataCompType) as ComponentData; //create instance returns an object initially. We need it to be a component type. Since it inherits from C.T. we can cast it.

                    if (comp == null)
                        return;

                    comp.InitializeAttackData(dataSO.NumberOfAttacks);


                    dataSO.AddData(comp);
                }
            }

        }

        showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update Buttons");// foldout (dropDownArrow) that opens contents "buttons" when the bool is true

        if (showForceUpdateButtons)
        {
            if (GUILayout.Button("Force Update Component Names"))
            {
                foreach (var item in dataSO.ComponentData)
                {
                    item.SetComponentName();
                }
            }
            if (GUILayout.Button("Force Update Attack Names"))
            {
                foreach (var item in dataSO.ComponentData)
                {
                    item.SetAttackDataNames();
                }
            }

        }
       
    }
    [DidReloadScripts] //attribute
    static void OnRecompile()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies(); // gets all assemblies currenlty loaded into application domain
        var types = assemblies.SelectMany(assembly => assembly.GetTypes()); // looks at each assembly in the array and calls get types. Takes that and puts it in another list "types"
        var filteredTypes = types.Where(
            type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );  // filters out the types (componentData) we care about that we need and not the ones that contain generics
        dataCompTypes = filteredTypes.ToList(); // converts them into a list. I think from Ienumberable
    
    }
}
