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

    private void OnEnable()
    {
                //casting the target object into the type we need. target is built in editor
        dataSO = target as WeaponDataSO;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        foreach (var dataCompType in dataCompTypes)
        {
            if (GUILayout.Button(dataCompType.Name))
            {//component data we want and instantiating a specific instance of that type. Activator to store info in a type class and create an object that has that type
                var comp = Activator.CreateInstance(dataCompType) as ComponentData; //create instance returns an object initially. We need it to be a component type. Since it inherits from C.T. we can cast it.

                if (comp == null)
                    return;

                dataSO.AddData(comp);
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
