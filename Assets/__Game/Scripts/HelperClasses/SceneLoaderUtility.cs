using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;
using UnityEngine.SceneManagement;
public class SceneLoaderUtility 
{
    public void LoadMenuScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
  public void LoadScene(string sceneName)
   {
        
        
       SaveLoadManager.Instance.SaveDataForSceneSwitch(); 
        if(GameStateTracker.Instance.CurrentGameState != GameState.Arena) //check before changing to next scene
        {
            GameManager.Instance.SavePlayerPartnerLocation();
        }
        if (sceneName == "BattleArena")
        {
            GameStateTracker.Instance.ChangeGameState(GameState.Arena);
        }
        else if (sceneName == "Overworld")
        {
            GameManager.Instance.SavePlayerPartnerLocation();

            GameStateTracker.Instance.ChangeGameState(GameState.overworld);

        }
        else if (sceneName == "SandBoxScene")
        {
           

            GameStateTracker.Instance.ChangeGameState(GameState.overworld);

        }
        CustomTransitionCall.Instance.LoadScene(sceneName);//implements transition then calls below method
      //  SceneManager.LoadScene(sceneName);
    
    }

    public string GetCurrentScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(SceneManager.GetSceneByBuildIndex(activeSceneIndex).isLoaded);
        string activeSceneName = SceneManager.GetSceneAt(activeSceneIndex).name;

        return activeSceneName;
    }


}


