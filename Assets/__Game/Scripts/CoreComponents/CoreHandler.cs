using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CoreHandler : MonoBehaviour
{
    private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();
    //making use of an interface that all the components can access
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
    // T is making use of a Generic where the type we've set it to be is the type we're looking for
    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = CoreComponents.OfType<T>().FirstOrDefault();

        if(comp== null)
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");

        }
        return comp;
    }


}
