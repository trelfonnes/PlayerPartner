using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : DataReferenceInheritor
{

   // public GameObject chosenPartnerCharacter;
  //  public GameObject chosenPlayerCharacter;// these are for spawning relations on scene switching
   // public GameObject[] playablePartnerPrefabs; // all creatures
   // public GameObject[] playablePlayerPrefabs; // male and female
   // int chosenPartnerCharacterIndex = 0;
    //int chosenPlayerCharacterIndex = 0;
   public PartnerType partnerFirstStageType;// this variable will be passed in by questionairre and set before passed to PartnerManager
    public PlayerType chosenPlayer;// this variable will be passed in by questionairre and set before passed to PartnerManager
                                                        // only needs to be the first stage of whatever partner.
    public ItemsObjectPool objectPool;
    public static GameManager Instance { get; private set; }
    IItemSpawnStrategy extraRareStrategy;
    IItemSpawnStrategy rareStrategy;
    IItemSpawnStrategy regularStrategy;
    PartnerManager partnerManager;
    PlayerManager playerManager;
    public BattleArenaDataSO currentNPCToBattle;
    public GameState CurrentGameState { get; private set; }
    GameData gameData = new GameData();
    protected override void Awake()
    {
        base.Awake();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerManager = GetComponentInChildren<PlayerManager>();

        partnerManager = GetComponentInChildren<PartnerManager>();
        
    }



    private void Start()
    {
        regularStrategy = new RegularItemSpawnStrategy(objectPool);
        rareStrategy = new RareItemSpawnStrategy(objectPool);
        extraRareStrategy = new ExtraRareItemSpawnStrategy(objectPool);
        InitializeChosenPartnerAndPlayer();
      

        // Set the desired strategy in the ItemSpawnSystem (you can do this based on game logic).
        // For example, based on the category of defeated enemy or broken object, you can set the strategy.
        ItemSpawnSystem.Instance.SetInitialItemSpawnStrategy(regularStrategy);
    }

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;

    }
    void InitializeChosenPartnerAndPlayer() //This is assuming Start will be called again between each scene, if not,
                                            //Delete the gameStateCheck and save load manager will do it.
    {
        if (CurrentGameState != GameState.Arena)
        {
            if (ES3.KeyExists("chosenPartner"))
            {
                partnerFirstStageType = ES3.Load<PartnerType>("chosenPartner");
            }
            if (ES3.KeyExists("chosenPlayer"))
            {
                chosenPlayer = ES3.Load<PlayerType>("chosenPlayer");
            }
            playerManager.SetPlayerType(chosenPlayer); //this will be passed in via the questionairre
            partnerManager.SetPartners(partnerFirstStageType);
        }
    }

    public void SwitchToRegularStrategy()
    {
        ItemSpawnSystem.Instance.ChangeItemSpawnStrategy(regularStrategy);
    }

    public void SwitchToRareStrategy()
    {
        ItemSpawnSystem.Instance.ChangeItemSpawnStrategy(rareStrategy);
    }
    public void SwitchToExtraRareStrategy()
    {
        ItemSpawnSystem.Instance.ChangeItemSpawnStrategy(extraRareStrategy);
    }
    public void SetPlayerAndPartnerType(PlayerType player, PartnerType partner) //call this from questionairre class
    {
        chosenPlayer = player;
        partnerFirstStageType = partner;
    }
    public void SetPlayer(Player player)
    {
        SaveLoadManager.Instance.SetPlayer(player);
    }
    public void SetPartner(GameObject currentPartner)
    {
        SaveLoadManager.Instance.SetPartner(currentPartner);
    }
   
    //Refactored these out
   // public void SetChosenCharacter(int PartnercharacterIndex, int PlayerCharacterIndex)
    //{
      //  chosenPartnerCharacterIndex = PartnercharacterIndex;
        //chosenPlayerCharacterIndex = PlayerCharacterIndex;
    //}
    
  //  public GameObject GetChosenPartnerPrefab()
  //  {
  //      return playablePartnerPrefabs[chosenPartnerCharacterIndex];
   // }
   // public GameObject GetChosenPlayerPrefab()
  //  {
  //      return playablePlayerPrefabs[chosenPlayerCharacterIndex];
  //  }

    //TODO: This will be the activate function found in the player and partner class
    // bool isChosenPlayer =GameManager.Instance.GetChosenPartnerPrefab() == gameObject;
    // bool isChosenPlayer = GameManager.Instance.GetChosenPlayerPrefab() == gameObject;
    //gameObject.SetActive(isChosenPlayer);
    
    //refactored out
    public void LoadNewScene(string sceneName, Transform playercurrentPosition) //call this from collider to enter next level and pass in the player and take its position and desired scene.
    {   //e.g. When I enter a next level trigger, it will call this method, pass in the "sceneToLoad" serialized field, and the position
        SavePlayerPosition(playercurrentPosition);
        SceneManager.LoadScene(sceneName);
    }
    void SavePlayerPosition(Transform player)//for saving previous scenes position
    {
        Vector3 playerPosition = player.position;
        PlayerPrefs.SetString("PlayerPosition", playerPosition.ToString());
    }
    public Vector3 GetSavedPosition()
    {
        string playerpositionString = PlayerPrefs.GetString("PlayerPosition", Vector3.zero.ToString());
        return StringToVector3(playerpositionString);
    }
    Vector3 StringToVector3(string s)
    {
        string[] split = s.TrimStart('(').TrimEnd(')').Split(',');
        return new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
    }

    public void SaveData(string key, object value)
    {
        gameData.AddData(key, value);
        SaveManager.Save();
    }
    public void LoadData()
    {
        SaveManager.Load();
    }
    public GameData GameData
    {
        get { return gameData; }
        set { gameData = value; }
    }

}
