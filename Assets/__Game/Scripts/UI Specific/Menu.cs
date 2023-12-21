using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    SceneLoaderUtility sceneLoader = new SceneLoaderUtility();
    string gameToLoad;
   public void NewGame()
    {
        sceneLoader.LoadScene("PlayerSelectScene");
    }

    public void Continue()
    {
        if (ES3.KeyExists("game1"))   //find out what all I need to save to this game1 to make it able to be loaded in at the position. 
                                        // also to look for multiple save files with easy save. So, everything that is saved is saved
                                           //under a specific save file. Then you have to load that save file to get access to the saved contents beneath it
                                           //load save file => then load specifics, latest scene, latest position, stats, etc. 
        {
            gameToLoad = "game1";
            LoadSavedGame(gameToLoad);
        }
      //  else
           // sceneLoader.LoadScene("PlayerSelectScene");
    }


    void LoadSavedGame(string gameToLoad)
    {
     //   SaveLoadManager.Instance.LoadWithEasySave();
        //ES3.Load(gameToLoad);
//        sceneLoader.LoadScene(gameToLoad);
    }
}
