using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameData gameData;
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;


    private PlayerInputManager inputManager;
    private PlayerData playerData;
    private Player player;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputManager = GetComponent<PlayerInputManager>();
        inputManager.onPlayerJoined += HandlePlayerJoined;
        SceneManager.sceneLoaded += HandleSceneLoaded;
        player = inputManager.playerPrefab.GetComponent<Player>();
        playerData = inputManager.playerPrefab.GetComponent<PlayerData>();
        player.Bind(playerData);
        GetStartingPlayerData();
        inputManager.playerPrefab = player2Prefab;
    }
    // TODO: if Player1 joins on button click and event shoots off when entering first level.
    //adjust the code to not do it on awake, but just to player1 within the function triggered onPlayerjoined

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "Menu")
        {
            inputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        }
        else
        {
            inputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;

        }
    }

    void HandlePlayerJoined(PlayerInput playerInput)
   {
        
          Debug.Log("HandlePlayerJoined " + playerInput);
        player = inputManager.playerPrefab.GetComponent<Player>();
        playerData = inputManager.playerPrefab.GetComponent<PlayerData>();
        player.Bind(playerData);
        GetPlayerData();
    }



    private PlayerData GetPlayerData()
    {
        if(gameData.PlayerDatas.Count <= 1)
        {
            Debug.Log("here");
            gameData.PlayerDatas.Add(playerData);
        }
        return gameData.PlayerDatas[1];
    }
    private PlayerData GetStartingPlayerData()
    {
        //if (_playerDatas.Count <= playerIndex)
        //{
        if (gameData.PlayerDatas.Count <= 0)
        {
            Debug.Log("here");
            //  var playerData = new PlayerData();
            gameData.PlayerDatas.Add(playerData);
        }
        return gameData.PlayerDatas[0];
    }

    public void NewGame()
    {
        gameData = new GameData();
        SceneManager.LoadScene("Level 1");
    }

}
