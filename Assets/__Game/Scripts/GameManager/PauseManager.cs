using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelCrushers.DialogueSystem;

public class PauseManager : MonoBehaviour
{
    public static event Action<bool> OnPauseStateChanged;
    static bool isPaused = false;
    public static bool IsPaused
    {
        get { return isPaused; }
        private set
        {
            isPaused = value;
            OnPauseStateChanged?.Invoke(isPaused);
        }
    }

    private void Awake()
    {
        if (IsPaused)
        {
            isPaused = false;
            IsPaused = false;
        }
    }
    public static void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
    }
    public void ToggleForDialogue(Transform actor)
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
    }
    private void OnEnable()
    {
       // DialogueManager.Instance.conversationEnded += ToggleForDialogue;
      //  DialogueManager.Instance.conversationStarted += ToggleForDialogue;
    }
    private void OnDisable()
    {
      //  DialogueManager.Instance.conversationEnded -= ToggleForDialogue;
       // DialogueManager.Instance.conversationStarted -= ToggleForDialogue;
    }

}
