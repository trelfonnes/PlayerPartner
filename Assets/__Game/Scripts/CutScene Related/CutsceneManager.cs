using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    public static event Action<bool> OnCutscenePlaying;
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
    public void PlayCutscene(CutsceneData cutsceneData)
    {
        isPlaying = true;
        //disable player input and other game controls by subbing to this and taking int the bool. 
        OnCutscenePlaying?.Invoke(isPlaying);
        cutsceneData.timeline.Play();

        cutsceneData.timeline.stopped += OnCutsceneEnd;
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        isPlaying = false;
        //Enable player input again and other game controls as needed
        OnCutscenePlaying?.Invoke(isPlaying);

        director.stopped -= OnCutsceneEnd;
    }
}
