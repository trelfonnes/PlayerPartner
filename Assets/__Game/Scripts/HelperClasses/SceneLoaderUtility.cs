using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoaderUtility 
{
  public void LoadScene(string sceneName)
   {
        SceneManager.LoadScene(sceneName);
    
    }

    public string GetCurrentScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(SceneManager.GetSceneByBuildIndex(activeSceneIndex).isLoaded);
        string activeSceneName = SceneManager.GetSceneAt(activeSceneIndex).name;

        return activeSceneName;
    }


}


