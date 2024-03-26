using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [SerializeField] private AudioClipManager audioClipManager; // Serialized field for AudioClipManager
    private AudioObjectPool audioObjectPool;

    Dictionary<string, GameObject> loopingObjects = new Dictionary<string, GameObject>();
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioObjectPool = GetComponent<AudioObjectPool>();

        InitializeAudioClips();
    }
    public void InitializeAudioClips()
    {
        if (audioClipManager != null)
        {
            foreach (var entry in audioClipManager.audioClips)
            {
                AddAudioClip(entry.id, entry.clip);
            }
        }
        else
        {
            Debug.LogWarning("AudioClipManager reference not set in AudioManager.");
        }
    }

    // Method to add audio clip to the dictionary
    public void AddAudioClip(string key, AudioClip clip)
    {
        if (!audioClips.ContainsKey(key))
        {
            audioClips.Add(key, clip);
        }
        else
        {
            Debug.LogWarning("Audio clip with key " + key + " already exists.");
        }
    }
    public void ReturnAudioToPool(GameObject audioObject)
    {
        Debug.Log("Return to the pool");
        audioObjectPool.ReturnToPool(audioObject);
    }

    // Method to play audio clip by key
    public void PlayAudioClip(string key)
    {
        if (audioClips.ContainsKey(key))
        {
            // Request audio playback GameObject from the pool
            GameObject audioObject = audioObjectPool.GetPooledObject();

            if (audioObject != null)
            {
                // Get AudioSource component and set the audio clip
                AudioSource audioSource = audioObject.GetComponent<AudioSource>();
                audioSource.clip = audioClips[key];
              //  audioObject.SetActive(true);
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Audio clip with key " + key + " not found.");
        }
    }
    public void PlayLoopingAudioClip(string key)
    {
        if (audioClips.ContainsKey(key))
        {
            // Request audio playback GameObject from the pool
            GameObject audioObject = audioObjectPool.GetLoopingPooledObject();
            AddToLoopingObjectList(key, audioObject);
            if (audioObject != null)
            {
                // Get AudioSource component and set the audio clip
                AudioSource audioSource = audioObject.GetComponent<AudioSource>();
                audioSource.clip = audioClips[key];
              //  audioObject.SetActive(true);
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Audio clip with key " + key + " not found.");
        }
    }
    void AddToLoopingObjectList(string key, GameObject audioObject) 
    {
        if (!loopingObjects.ContainsKey(key))
        {
            loopingObjects.Add(key, audioObject);
        }
    }

    
    public void TurnLoopingObjectOff(string audioName)
    {
        if (loopingObjects.ContainsKey(audioName))
        {
            // Get the GameObject associated with the audio name
            GameObject audioObject = loopingObjects[audioName];

            // Set the GameObject to inactive and turn audio off
            audioObject.SetActive(false);

            // Remove the GameObject from the dictionary so it can be added as the manager sets it with a new clip/keyword
            loopingObjects.Remove(audioName);
        }
        else
        {
            Debug.LogWarning("No looping audio object with the specified name found: " + audioName);
        }
    }
}
