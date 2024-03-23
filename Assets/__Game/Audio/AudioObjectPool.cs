using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjectPool : MonoBehaviour
{
    public GameObject audioPlaybackPrefab;

    // Maximum number of audio playback GameObjects in the pool
    public int maxPoolSize = 10;

    // List to store available audio playback GameObjects
    private List<GameObject> availableObjects = new List<GameObject>();

    // List to store in-use audio playback GameObjects
    private List<GameObject> inUseObjects = new List<GameObject>();

    private void Start()
    {
        // Preload audio playback GameObjects into the pool
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject obj = Instantiate(audioPlaybackPrefab);
            obj.SetActive(false);
            availableObjects.Add(obj);
        }
    }

    // Method to get an audio playback GameObject from the pool
    public GameObject GetPooledObject()
    {
        if (availableObjects.Count == 0)
        {
            // If no available objects in the pool, dynamically create a new one if the pool size limit is not reached
            if (inUseObjects.Count < maxPoolSize)
            {
                GameObject newObj = Instantiate(audioPlaybackPrefab);
                newObj.SetActive(false);
                inUseObjects.Add(newObj);
                return newObj;
            }
            else
            {
                // If the pool size limit is reached, return null
                Debug.LogWarning("AudioObjectPool limit reached. Consider increasing the maxPoolSize.");
                return null;
            }
        }
        else
        {
            // Retrieve an available object from the pool
            GameObject obj = availableObjects[0];
            availableObjects.RemoveAt(0);
            inUseObjects.Add(obj);
            return obj;
        }
    }

    // Method to return an audio playback GameObject to the pool
    public void ReturnToPool(GameObject obj)
    {
        if (inUseObjects.Contains(obj))
        {
            obj.SetActive(false);
            inUseObjects.Remove(obj);
            availableObjects.Add(obj);
        }
        else
        {
            Debug.LogWarning("Trying to return an object to AudioObjectPool that was not obtained from the pool.");
        }
    }
}
