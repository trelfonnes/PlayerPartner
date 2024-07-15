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
    public void LoadContinueFromGameOver(string sceneName)
    {
        if(sceneName == "SandBoxScene")
        {
            GameStateTracker.Instance.ChangeGameState(GameState.overworld);

        }
        SceneManager.LoadScene(sceneName);
    }
  public void LoadScene(string sceneName)
   {

        if (sceneName != "GameOver" && GameStateTracker.Instance.CurrentGameState != GameState.gameOver)
        {


            SaveLoadManager.Instance.SaveDataForSceneSwitch();
        }
        if(GameStateTracker.Instance.CurrentGameState != GameState.Arena && sceneName != "GameOver") //check before changing to next scene
        {
            GameManager.Instance.SavePlayerPartnerLocation();
        }
        if (sceneName == "BattleArena")
        { 
            GameStateTracker.Instance.ChangeGameState(GameState.Arena);
            CustomTransitionCall.Instance.ChangeTransitionToCircleFade();
            CustomTransitionCall.Instance.startDelay = .25f;

        }
        else if (sceneName == "Overworld")
        {
            GameManager.Instance.SavePlayerPartnerLocation(); //save last overworld position before leaving it

            GameStateTracker.Instance.ChangeGameState(GameState.overworld);
            CustomTransitionCall.Instance.ChangeTransitionToCircleFade();
            CustomTransitionCall.Instance.startDelay = .25f;

        }
        else if (sceneName == "SandBoxScene")
        {

            CustomTransitionCall.Instance.ChangeTransitionToCircleFade();

            GameStateTracker.Instance.ChangeGameState(GameState.overworld);
            CustomTransitionCall.Instance.startDelay = .25f;

        }
        else if (sceneName == "GameOver")
        {
            GameStateTracker.Instance.ChangeGameState(GameState.gameOver);
            CustomTransitionCall.Instance.ChangeTransitionToFade();
            CustomTransitionCall.Instance.startDelay = 2.5f;

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


