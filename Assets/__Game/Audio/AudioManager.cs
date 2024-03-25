using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [SerializeField] private AudioClipManager audioClipManager; // Serialized field for AudioClipManager
    private AudioObjectPool audioObjectPool;
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
            Debug.Log("BEFORE AUDIO OBJECT NULL CHECK");

            if (audioObject != null)
            {
                Debug.Log("PLAY THE AUDIO CLIP");
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
}
