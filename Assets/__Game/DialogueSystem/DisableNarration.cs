using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DisableNarration : MonoBehaviour
{
    [SerializeField] string nameOfNarration;
    bool hasBeenTriggered;

    private void Start()
    {
        if (ES3.KeyExists(nameOfNarration))
        {
            hasBeenTriggered = ES3.Load<bool>(nameOfNarration);
        }
        if (hasBeenTriggered)
        {
            NarrationOff();
        }
    }
    void NarrationOff()
    {
        hasBeenTriggered = true;
        ES3.Save(nameOfNarration, hasBeenTriggered);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Lua.RegisterFunction("NarrationOff", this, SymbolExtensions.GetMethodInfo(() => NarrationOff()));

    }
    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(NarrationOff));

    }
}
