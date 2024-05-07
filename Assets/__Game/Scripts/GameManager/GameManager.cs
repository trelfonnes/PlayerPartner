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
  // public PartnerType partnerFirstStageType;// this variable will be passed in by questionairre and set before passed to PartnerManager
    public PlayerType chosenPlayer;// this variable will be passed in by questionairre and set before passed to PartnerManager
    [SerializeField] Vector2 savedLocations;    // only needs to be the first stage of whatever partner.
    public ItemsObjectPool objectPool;
    IItemSpawnStrategy extraRareStrategy;
    IItemSpawnStrategy rareStrategy;
    IItemSpawnStrategy regularStrategy;
    PartnerManager partnerManager;
    PlayerManager playerManager;
    public BattleArenaDataSO currentNPCToBattle;
   // public GameState CurrentGameState { get; private set; }
    GameData gameData = new GameData();
    public static GameManager Instance { get; private set; }

    public Player CurrentPlayer;
    GameObject CurrentPartner;
    ProgressMarker currentGameProgress;

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
    private void OnEnable()
    {
        SaveLoadManager.onGlobalLoad += GlobalLoadListener;
        SaveLoadManager.onGlobalSave += GlobalSaveListener;
    }
    private void OnDisable()
    {
        if (TimeOfDayManager.Instance)
        {
            TimeOfDayManager.Instance.DisableGlobalLight();

            if (GameStateTracker.Instance.CurrentGameState == GameState.overworld)
            {
                //SaveLoadManager.Instance.SaveGlobalData();  SAVE CAN"T BE CALLED ONDISABLE. MUST BE DONE FROM SCENE SWITCH INVOKING OBJECT
                // SaveLoadManager.Instance.SaveLastPlayerPosition();
            }
            SaveLoadManager.onGlobalLoad -= GlobalLoadListener;
            SaveLoadManager.onGlobalSave -= GlobalSaveListener;
        }
    }
    void GlobalLoadListener()
    {
        if (ES3.KeyExists("currentGameProgress"))
        {
          currentGameProgress = ES3.Load<ProgressMarker>("currentGameProgress");
        }
    }
    void GlobalSaveListener()
    {
        ES3.Save<ProgressMarker>("currentGameProgress", currentGameProgress);
    }


    private void Start()
    {
        InitializeChosenPlayer();
        if (GameStateTracker.Instance.CurrentGameState != GameState.Arena)
        {
            InitializeChosenPartner();
           // SaveLoadManager.Instance.LoadBattleArenaData();
            SaveLoadManager.Instance.SaveCurrentScene();

        } 
        if (GameStateTracker.Instance.CurrentGameState == GameState.Arena)
        {
           // InitializeChosenPartner();
            SaveLoadManager.Instance.LoadBattleArenaData();
          //  SaveLoadManager.Instance.SaveCurrentScene();

        }

        if (GameStateTracker.Instance.CurrentGameState == GameState.overworld)
        {
            LoadPlayerPartnerLocation();
            SaveLoadManager.Instance.LoadGlobalData();
        }
        regularStrategy = new RegularItemSpawnStrategy(objectPool);
        rareStrategy = new RareItemSpawnStrategy(objectPool);
        extraRareStrategy = new ExtraRareItemSpawnStrategy(objectPool);
   
        // Set the desired strategy in the ItemSpawnSystem (you can do this based on game logic).
        // For example, based on the category of defeated enemy or broken object, you can set the strategy.
        ItemSpawnSystem.Instance.SetInitialItemSpawnStrategy(regularStrategy);
        TimeOfDayManager.Instance.EnableGlobalLight();
       
    }

  
    void InitializeChosenPlayer() 
    {
        if (GameStateTracker.Instance.CurrentGameState != GameState.Arena || GameStateTracker.Instance.CurrentGameState != GameState.gameOver)
        {
           
            if (ES3.KeyExists("chosenPlayer"))
            {
                chosenPlayer = ES3.Load<PlayerType>("chosenPlayer");
            }
            playerManager.SetPlayerType(chosenPlayer); 
        }
    } 
    void InitializeChosenPartner() 
    {
       
           if (ES3.KeyExists("chosenPartner"))
            {
                partnerFirstStageType = ES3.Load<PartnerType>("chosenPartner");
            }
       
            partnerManager.SetPartners(partnerFirstStageType);
        
    }
    
   public void SavePlayerPartnerLocation()
    {
        Debug.Log(CurrentPlayer + "This is the player");
        SaveLoadManager.Instance.SaveLastPlayerPosition();

    }
    void LoadPlayerPartnerLocation() // call this after things have been initialized.
    {
        if (ES3.KeyExists("playerPartnerLocation"))
        {
            savedLocations = SaveLoadManager.Instance.LoadLastPlayerPosition();
            playerManager.MoveThePlayer(savedLocations);
            partnerManager.MoveCurrentPartner(savedLocations);//pass in saved locations to both
        }
        else
        {
            Debug.LogWarning("Key 'playerPartnerLocation' does not exist or data is not loaded.");

        }
    }
    public ProgressMarker ReturnCurrentGameProgress()
    {
        return currentGameProgress;
    }
    public void ChangeCurrentGameProgress(ProgressMarker progressMarker)
    {
        currentGameProgress = progressMarker;
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
    public void SetPlayerInSaveManager(Player player)
    {
        CurrentPlayer = player;

        SaveLoadManager.Instance.SetPlayer(player);
    }
    public void SetPartnerInSaveManager(PartnerType currentPartner)
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
public enum ProgressMarker
{
    act1,
    act2,
    act3,
    act4
}
