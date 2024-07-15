using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BossComponentLocator : MonoBehaviour
{
    private readonly List<BossCoreComponent> BossCoreComponents = new List<BossCoreComponent>();

    private void Awake()
    {
        
    }

    public void AddComponent(BossCoreComponent component)
    {
        if (!BossCoreComponents.Contains(component))
        {
            BossCoreComponents.Add(component);
        }
    }
    public T GetBossCoreComponent<T>() where T : BossCoreComponent
    {
        var comp = BossCoreComponents.OfType<T>().FirstOrDefault();

        if (comp)
        {
            return comp;
        }
        else
        {
            comp = GetComponentInChildren<T>();
        }
        if (comp)
        {
            return comp;
        }
        else
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }
        return null;
    }

    public T GetCoreComponent<T>(ref T value) where T: BossCoreComponent
    {
        value = GetBossCoreComponent<T>();
        return value;
    }
}
