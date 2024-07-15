using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneSetup : MonoBehaviour
{
    public string cutsceneName; // Identifier for the cutscene
    public PlayableDirector director; // The PlayableDirector in the scene

    private void Start()
    {
        if (CutsceneManager.Instance != null)
        {
            CutsceneManager.Instance.RegisterCutscene(cutsceneName, director);
        }
    }
}
