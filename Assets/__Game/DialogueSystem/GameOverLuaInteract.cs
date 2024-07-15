using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class GameOverLuaInteract : MonoBehaviour
{
    SceneLoaderUtility sceneLoader = new SceneLoaderUtility();
    public void ChangeScene(string sceneName)
    {
        if(sceneName == "Menu")
        {
            sceneLoader.LoadMenuScene(sceneName);
        }
        else
        {
            Debug.Log("Change the scene from dialogue selcet game over" + sceneName);
            sceneLoader.LoadContinueFromGameOver(sceneName);
        }
    }

    private void OnEnable()
    {
        Lua.RegisterFunction("ChangeScene", this, SymbolExtensions.GetMethodInfo(() => ChangeScene(string.Empty)));


    }
    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(ChangeScene));        
    }
}
