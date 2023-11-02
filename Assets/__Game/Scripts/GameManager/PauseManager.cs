using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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


}
