using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    private Dictionary<string, PlayableDirector> cutsceneDictionary = new Dictionary<string, PlayableDirector>();
    public static event Action<bool> OnCutscenePlaying;
    public static event Action onCutsceneFinished;
    bool isPlaying;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RegisterCutscene(string cutsceneName, PlayableDirector director)
    {
        cutsceneDictionary[cutsceneName] = director;
    }

    public void PlayCutscene(string cutsceneName)
    {
        if (cutsceneDictionary.TryGetValue(cutsceneName, out PlayableDirector director))
        {
            Debug.Log("Play the cutscene");
            isPlaying = true;
            OnCutscenePlaying?.Invoke(isPlaying);
            director.stopped += OnCutsceneEnd;
            director.Play();
        }
        else
        {
            Debug.LogError($"Cutscene '{cutsceneName}' not found in the current scene.");
        }
    }

   
    void OnCutsceneEnd(PlayableDirector director)
    {
        Debug.Log("Cutscene has ended, start the battle");
        isPlaying = false;
        //Enable player input again and other game controls as needed
        //  OnCutscenePlaying?.Invoke(isPlaying);
        DeactivateCutsceneElement(director);
        director.stopped -= OnCutsceneEnd;
    }

    void DeactivateCutsceneElement(PlayableDirector director)
    {
        Debug.Log("Deactivate the director's game object" + director.gameObject.name);
        OnCutscenePlaying?.Invoke(isPlaying);
       if(onCutsceneFinished != null)
        {
            onCutsceneFinished?.Invoke();
        }
    }
}
