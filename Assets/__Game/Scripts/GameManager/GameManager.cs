using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : DataReferenceInheritor
{
    public static GameManager Instance { get; private set; }

    public List<string> AllGameNames = new List<string>();

   // [SerializeField] GameData gameData;
    [SerializeField] protected GameObject player1Prefab;
    [SerializeField] protected GameObject player2Prefab;
    [SerializeField] protected GameObject partner1Prefab;

    private PlayerInputManager inputManager;
    private PlayerData playerData;
   
    private Player player;
    protected override void Awake()
    {
        base.Awake();
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputManager = GetComponent<PlayerInputManager>();
      //  inputManager.onPlayerJoined += HandlePlayerJoined;
     //   SceneManager.sceneLoaded += HandleSceneLoaded;
      //  player = inputManager.playerPrefab.GetComponent<Player>();
        //playerData = new PlayerData();   
       // player.Bind(playerData);
        //GetStartingPlayerData();
        //inputManager.playerPrefab = player2Prefab;
       // string commaSeparatedList = PlayerPrefs.GetString("AllGameNames");
        //AllGameNames = commaSeparatedList.Split(",").ToList();
    
        }
    // TODO: if Player1 joins on button click and event shoots off when entering first level.
    //adjust the code to not do it on awake, but just to player1 within the function triggered onPlayerjoined
  //  void HandlePlayerJoined(PlayerInput playerInput)
   // {

      
    //    player = inputManager.playerPrefab.GetComponent<Player>();
    //    playerData = new PlayerData();
    //    player.Bind(playerData);
    //    GetPlayerData(); 
   // }
  //  private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
   // {
      //  if(arg0.name == "Menu")
     //   {
          //  inputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        //}
       // else
       // {
        //    inputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
       //     SaveGameOld();
      //  }

    //}

   // void SaveGameOld()
   // {gameData
     //   string text = JsonUtility.ToJson(gameData);

     //   if (AllGameNames.Contains(gameData.GameName) == false) 
     //   { 
     //       AllGameNames.Add(gameData.GameName); 
     //   }
        //gamenames
      //  string commaSeparatedGameNames = string.Join(",", AllGameNames);

     //   PlayerPrefs.SetString("AllGameNames", commaSeparatedGameNames);
      //  PlayerPrefs.SetString(gameData.GameName, text);
       // PlayerPrefs.Save();
   // }//

   // public void LoadGameOld()
    //{
       // string text = PlayerPrefs.GetString("Game1");
       // gameData = JsonUtility.FromJson<GameData>(text);
       // SceneManager.LoadScene("level 1");
    //}
  



  //  private PlayerData GetPlayerData()
    //{
      //  if(gameData.PlayerDatas.Count <= 1)
        //{
          //  gameData.PlayerDatas.Add(playerData);
        //}
        //return gameData.PlayerDatas[1];
    //}
    //private PlayerData GetStartingPlayerData()
    //{
        //if (_playerDatas.Count <= playerIndex)
        //{
      //  if (gameData.PlayerDatas.Count <= 0)
        //{
            //  var playerData = new PlayerData();
          //  gameData.PlayerDatas.Add(playerData);
        //}
        //return gameData.PlayerDatas[0];
    //}

    //public void NewGame()
    //{
      //  gameData = new GameData();
        //gameData.GameName = DateTime.Now.ToString("G");
        //SceneManager.LoadScene("Level 1");
    //}

}
