using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightInScene : MonoBehaviour
{
    // Singleton instance
    private static GlobalLightInScene instance;

    private void Awake()
    {
        Debug.Log("Awake of GlobalLightManager" + gameObject.name);

        // Ensure there's only one instance of the singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GlobalLightManager instance created.");

        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
            Debug.Log("Duplicate GlobalLightManager instance destroyed.");

        }

        // Your Awake code here, if needed
    }
}
