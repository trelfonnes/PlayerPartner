using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//test for git change pickup
public class CoreHandler : MonoBehaviour
{
    private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();
  
    private void Awake()
    {
    }

  
    public void LogicUpdate()
    {
        foreach (CoreComponent component in CoreComponents)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!CoreComponents.Contains(component))
        {
            CoreComponents.Add(component);

        }
    }
    //service locator pattern
    // T is making use of a Generic where the type we've set it to be is the type we're looking for
    public T GetCoreComponent<T>() where T : CoreComponent
    {

        var comp = CoreComponents.OfType<T>().FirstOrDefault();//takes the first element of the collection
                                                               //passed in as type T. or returns default value(null) if nothing is found

        if (comp)
            return comp;
        else
        {
            comp = GetComponentInChildren<T>();
        }
        if (comp)
           return comp;
       else
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }
            return null;
    }

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {

        value = GetCoreComponent<T>();
        return value;
    }

}
