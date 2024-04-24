using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System;

public class SaveLoadManager : DataReferenceInheritor
{
    PlayerSOData playerBasicData;
    PlayerSOData partner1BasicData;
    PlayerSOData partner2BasicData;
    PlayerSOData partner3BasicData;
    [SerializeField] PlayerData sharedPlayerData;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] PlayerArtifactInventory artifactInventory;
    [SerializeField] List<inventoryItems> itemsAmountInInventory;
    [SerializeField] GameObject gameSavedUI;
    [SerializeField] WeaponInventoryManager weaponInventoryManager;
   [SerializeField] List<WeaponInventoryItemSO> playerWeapons;
   [SerializeField] List<WeaponInventoryItemSO> partnerWeapons;
    [SerializeField] Player player;
    [SerializeField] Partner partner;
    [SerializeField] Vector2 savedLocations;
    [SerializeField] Transform locationForPartnerLoad;
   [SerializeField] NPCManager npcManager;
    SceneLoaderUtility sceneLoader = new SceneLoaderUtility();

    public static event Action onGlobalSave;
    public static event Action onGlobalLoad;

    [SerializeField] PartnerType activePartner;

    public static SaveLoadManager Instance;

    void TriggerGlobalSaveEvent()
    {
        onGlobalSave?.Invoke();
    }
    void TriggerGlobalLoadEvent()
    {
        onGlobalLoad?.Invoke();
    }
   
    public void InitializeNPCManager(NPCManager npcManager)
    {
        this.npcManager = npcManager;
       // LoadNPCData();
    }
    public void InitializeSharedPlayerData(PlayerData playerData)
    {
        sharedPlayerData = playerData;
    }
    protected override void Awake()
    {
        base.Awake();
        playerBasicData = playerSOData;
        partner1BasicData = partner1SOData;
        partner2BasicData = partner2SOData;
        partner3BasicData = partner3SOData;

        if (Instance == null)
        {
            Instance = this;
        }   
        else if( Instance != this)
        {
            Destroy(this);

        }
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
      // LoadWithEasySave();

    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetPartner(PartnerType currentPartner)
    {
        activePartner = currentPartner;
       
    }
    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
        
    }
   public void SaveDataForSceneSwitch() //can't sub to scenemanager unloaded so call from utility here.
    {
        if(GameStateTracker.Instance.CurrentGameState == GameState.Arena)
        {
            SaveDataFromBattleArena();
        }
        else
        {
            SaveGlobalData();
        }
    }

     
    
    public void LoadDataFromSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleArena") // check the incomming scene, set state accordingly
        {
            GameStateTracker.Instance.ChangeGameState(GameState.Arena); //when arena is entered, I load just persistant data
        }
        else if(scene.name == "Dungeon") //Might not need this.
        {
            GameStateTracker.Instance.ChangeGameState(GameState.Dungeon);
        }
        else
        {
            GameStateTracker.Instance.ChangeGameState(GameState.overworld);
        }

       
    }
    public void SaveGlobalData() //Globals to be called when all data is needed. i.e. starting the game or returning to the overworld/ anywhere with all needed functionality
    {  // before exiting overworld. Or use for an autosave, etc. NOT during a battlescene.
        TriggerGlobalSaveEvent();
           SavePlayerPartnerBasicData();
        SaveSharedPartnerData();
        SavePlayerInventoryContents();
        SaveWeaponItems();
       // SaveLastPlayerPosition(); //
        SaveLastActivePartner();
        //PopUpUI(); call this during Player save, not auto save between scenes
      
    }
    public void LoadSavedSceneFromMenu() //Call this from the menu...
    {
        LoadCurrentScene();

    }
    public void LoadGlobalData()// return to overworld or load game from menu.
    {
        TriggerGlobalLoadEvent();
        LoadPlayerPartnerBasicData();
        //LoadSharedPartnerData();
        LoadPlayerInventoryContents(); 
        LoadWeaponItems();
        //LoadPlayerPartnerLocation(); this needs to be done after initialized or this class won't have reference to player or partner
        LoadLastActivePartner();
        LoadChosenPlayerAndPartner();
       // LoadNPCData();
    }

    public void LoadChosenPlayerAndPartner()
    {
        if (ES3.KeyExists("chosenPartner"))
        {
            GameManager.Instance.partnerFirstStageType = ES3.Load<PartnerType>("chosenPartner");
        }
        if (ES3.KeyExists("chosenPlayer"))
        {
            GameManager.Instance.chosenPlayer = ES3.Load<PlayerType>("chosenPlayer");
        }
    }
    public void SaveDataFromBattleArena() // these will be called right before leaving the Arena, to update any stat boosts or items received from the battle
    {
        SavePlayerPartnerBasicData();
        SaveSharedPartnerData();
        SaveWeaponItems();
        SaveNPCData();

    }
    public void LoadBattleArenaData() //This will be called right before entering the arena, to persist data needed for the battle such as stats
    {
        LoadPlayerPartnerBasicData();
       // LoadSharedPartnerData();
        LoadWeaponItems();
    }

    void SaveNPCData()
    {
        //add future NPC related data to persist here by accessing injected NPCManager;
        List<BattleArenaDataSO> listToSave = npcManager.GetListOfNPCSOs();
        ES3.Save("NPCBattleList", listToSave);
    }
    void LoadNPCData()
    {
        //load back into NPC Manager. Clears list, then receives the saved one.
        if (ES3.KeyExists("NPCBattleList"))
        {
            List<BattleArenaDataSO> listToLoad = ES3.Load<List<BattleArenaDataSO>>("NPCBattleList");
            npcManager.RestoreSavedList(listToLoad);
        }
    }
   public void SaveCurrentScene() 
    {
        string scene = sceneLoader.GetCurrentScene();
       // string sceneToSave = sceneLoader.GetCurrentScene();
        ES3.Save("currentScene", scene);
    }
    void LoadCurrentScene()//call this from initial load only at game starting up. Don't need to between scene transitions
    {
        if (ES3.KeyExists("currentScene"))
        {
            string sceneToLoad = ES3.Load<string>("currentScene");
            sceneLoader.LoadScene(sceneToLoad);
        }
    }
    private void SavePlayerPartnerBasicData()
    {
        ES3.Save("playerDataSO", playerBasicData);
        ES3.Save("partner1BasicData", partner1SOData);
        ES3.Save("partner2BasicData", partner2SOData);
        ES3.Save("partner3BasicData", partner3SOData);
        

    }
    private void LoadPlayerPartnerBasicData()
    {
        ES3.Load("playerDataSO", playerBasicData);
        ES3.Load("partner1BasicData", partner1SOData);
        ES3.Load("partner2BasicData", partner2SOData);
        ES3.Load("partner3BasicData", partner3SOData);
    }
    void SaveSharedPartnerData() //AKA PlayerData
    {
        ES3.Save("sharedPartnerData", sharedPlayerData);

    }
    public PlayerData LoadSharedPartnerData()
    {
        if (sharedPlayerData != null)
        {
            sharedPlayerData = ES3.Load<PlayerData>("sharedPartnerData");
            return sharedPlayerData;
        }
        else { return null; }

        }
        void SavePlayerInventoryContents()
    {
        ES3.Save("playerInventory", playerInventory);
        ES3.Save("individualInventoryItems", itemsAmountInInventory);
        ES3.Save("artifactInventory", artifactInventory);
    } 
   
    void LoadPlayerInventoryContents()
    {
        if (ES3.KeyExists("playerInventory"))
        {
            ES3.Load("playerInventory");
        }
        if (ES3.KeyExists("individualInventoryItems"))
        {
            ES3.Load("individualInventoryItems");
        }
        if (ES3.KeyExists("artifactInventory"))
        {
            ES3.Load("artifactInventory");
        }
    }
    void SaveWeaponItems()
    {
        Debug.Log("SAVE WEAPON ITEMS" + playerWeapons + partnerWeapons);
        playerWeapons = weaponInventoryManager.playerWeaponsInInventory;
        partnerWeapons = weaponInventoryManager.partnerWeaponsInInventory;

        ES3.Save("playerWeapons", playerWeapons);
        ES3.Save("partnerWeapons", partnerWeapons);
    }
    void LoadWeaponItems()
    {
        Debug.Log("LOAD WEAPON ITEMS" + playerWeapons + partnerWeapons);

        if (ES3.KeyExists("playerWeapons"))
        {
            // Load playerWeapons data
            playerWeapons = ES3.Load<List<WeaponInventoryItemSO>>("playerWeapons");
        }
        else
        {
            // Handle the case when the key doesn't exist or the data is not loaded
            Debug.LogWarning("Key 'playerWeapons' does not exist or data is not loaded.");
        }
        if (ES3.KeyExists("partnerWeapons"))
        {
            // Load partnerWeapons data
            partnerWeapons = ES3.Load<List<WeaponInventoryItemSO>>("partnerWeapons");
        }
        else
        {
            // Handle the case when the key doesn't exist or the data is not loaded
            Debug.LogWarning("Key 'partnerWeapons' does not exist or data is not loaded.");
        }
       
        weaponInventoryManager.playerWeaponsInInventory = playerWeapons;
        weaponInventoryManager.partnerWeaponsInInventory = partnerWeapons;
        weaponInventoryManager.ClearAndMakeSlots();
    
    }
     
    
    void SaveLastActivePartner()
    {
        ES3.Save("lastActivePartner", activePartner);
    }
    void LoadLastActivePartner()
    {
        if (ES3.KeyExists("lastActivePartner"))
        {
            activePartner = ES3.Load<PartnerType>("lastActivePartner");
           

            PartnerManager.Instance.SetLastPartnerActive(activePartner);
            
        }
        else
        {
            Debug.LogError("lastActivePartner key does not exist");
        }
    }
    public void SaveLastPlayerPosition()
    {
        if (player)
        {
            savedLocations = player.transform.position;

            ES3.Save("playerPartnerLocation", savedLocations);
        }
    }
    public Vector2 LoadLastPlayerPosition()
    {
        if (ES3.KeyExists("playerPartnerLocation"))
        {
            savedLocations = ES3.Load<Vector2>("playerPartnerLocation");
            return savedLocations;
            //pass in saved locations to both
        }
        else return Vector2.zero;
       
    }
    public void SaveBool(string key, bool value)
    {
        ES3.Save<bool>(key, value);
    }
    public bool LoadBool(string key)
    {
        if (ES3.KeyExists(key))
        {
            return ES3.Load<bool>(key);
        }
        else return false;
    }

    public void SaveGame()
    {// add saving PlayerInventory scriptable object
     //base folder
        Debug.Log(playerBasicData.name);

        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        //player folder
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/Player_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/Player_Data");
        }
        //partner folder
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/Partner_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/Partner_Data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        // can probably loop through these to create one for each...
        //for Player
        FileStream filePlayer = File.Create(Application.persistentDataPath + "/game_save/Player_Data/Player_Data_SaveFilePlayer");
        //convert data here:

        var json = JsonUtility.ToJson(playerBasicData);
        bf.Serialize(filePlayer, json);
        filePlayer.Close();
        
        //ForPartners
        FileStream filePartner1 = File.Create(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner1");
        var json2 = JsonUtility.ToJson(partner1BasicData);
        bf.Serialize(filePartner1, json2);
        filePartner1.Close();
        
        FileStream filePartner2 = File.Create(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner2");
        var json3 = JsonUtility.ToJson(partner2BasicData);
        bf.Serialize(filePartner2, json3);
        filePartner2.Close(); 
        
        FileStream filePartner3 = File.Create(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner3");
        var json4 = JsonUtility.ToJson(partner3BasicData);
        bf.Serialize(filePartner3, json4);
        filePartner3.Close();
        //ForInventory



        // TODO make "Game Saved" UI pop up
        PopUpUI();
    }

    private void PopUpUI()
    {
        if (gameSavedUI)
        {
            gameSavedUI.SetActive(true);
            StartCoroutine(HideGameSavedUI());
        }
    }
    IEnumerator HideGameSavedUI()
    {
        yield return new WaitForSeconds(2f);
        if (gameSavedUI)
        {
            gameSavedUI.SetActive(false);
        }
    }

    public void LoadGame()
    {
        //add Load PlayerInventory Scriptable Object
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/Player_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/Player_Data");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/Partner_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/Partner_Data");
        }
        BinaryFormatter bf = new BinaryFormatter();
        //for Player
        if(File.Exists(Application.persistentDataPath + "/game_save/Player_Data/Player_Data_SaveFilePlayer"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/Player_Data/Player_Data_SaveFilePlayer", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerBasicData);
            file.Close();
        }
        //for partners
        if (File.Exists(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner1"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner1", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), partner1BasicData);
            file.Close();

        }
        if (File.Exists(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner2"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner2", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), partner2BasicData);
            file.Close();

        }
        if (File.Exists(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner3"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner3", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), partner3BasicData);
            file.Close();

        }
        //for inventory
    }



}
