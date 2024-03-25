using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjectPool : MonoBehaviour
{
    public GameObject audioPlaybackPrefab;
    public int maxPoolSize = 10;

    private List<GameObject> pooledAudioObjects = new List<GameObject>();

    private void Start()
    {
        // Preload audio playback GameObjects into the pool
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject obj = Instantiate(audioPlaybackPrefab);
            obj.SetActive(false);
            pooledAudioObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        // Find and return an inactive audio playback GameObject from the pool
        foreach (GameObject obj in pooledAudioObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive object is found, return null
        Debug.LogWarning("No available audio playback GameObjects in the pool.");
        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        // Deactivate the audio playback GameObject and return it to the pool
        obj.SetActive(false);
    }
}
