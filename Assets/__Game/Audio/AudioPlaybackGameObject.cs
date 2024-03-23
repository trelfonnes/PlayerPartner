using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlaybackGameObject : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] float delayBeforeCheck = 0.1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Start coroutine to check audio playback completion after a delay
        StartCoroutine(CheckAudioPlaybackCompletion());
    }

    private IEnumerator CheckAudioPlaybackCompletion()
    {
        // Wait for the specified delay before checking audio playback
        yield return new WaitForSeconds(delayBeforeCheck);

        // Check if audio playback is completed
       
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Audio playback is completed, return GameObject to the pool
        AudioManager.Instance.ReturnAudioToPool(gameObject);
    }
    public void SetAudioClip(AudioClip clip) // this method might not be needed but it is here just in case.
    {
        audioSource.clip = clip;
    }
}
