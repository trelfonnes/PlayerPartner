using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveLoadManager : DataReferenceInheritor
{
    PlayerSOData playerBasicData;
    PlayerSOData partner1BasicData;
    PlayerSOData partner2BasicData;
    PlayerSOData partner3BasicData;
    [SerializeField] PlayerData sharedPartnerData;
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
    NPCManager npcManager;
    SceneLoaderUtility sceneLoader = new SceneLoaderUtility();

    

    [SerializeField] GameObject activePartner;

    public static SaveLoadManager Instance;

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += SaveDataFromSceneUnloaded;
        SceneManager.sceneLoaded += LoadDataFromSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= SaveDataFromSceneUnloaded;
        SceneManager.sceneLoaded -= LoadDataFromSceneLoaded;
    }
    public void InitializeNPCManager(NPCManager npcManager)
    {
        this.npcManager = npcManager;
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

    public void SetPartner(GameObject currentPartner)
    {
        activePartner = currentPartner;
        Partner partner = currentPartner.GetComponent<Partner>();
        this.partner = partner;
    }
    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
        
    }
    public void SaveDataFromSceneUnloaded(Scene scene)
    {
        if (scene.name == "BattleArena")
        {
            GameManager.Instance.SetGameState(GameState.Arena);// when arena is unloaded, I save just the persistant data
        }
        else
        {
            GameManager.Instance.SetGameState(GameState.overworld);
        }

        if (GameManager.Instance.CurrentGameState == GameState.Arena)
        {
            SaveDataFromBattleArena();
        }
        else
        {
            SaveGlobalData();
            SaveCurrentScene(scene);
        }
    }
    public void LoadDataFromSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleArena") // check the incomming scene, set state accordingly
        {
            GameManager.Instance.SetGameState(GameState.Arena); //when arena is entered, I load just persistant data
        }
        else
        {
            GameManager.Instance.SetGameState(GameState.overworld);
        }

        if (GameManager.Instance.CurrentGameState == GameState.Arena)
        {
            LoadBattleArenaData();
        }
        else
        {
            LoadGlobalData();
        }
    }
    public void SaveGlobalData() //Globals to be called when all data is needed. i.e. starting the game or returning to the overworld/ anywhere with all needed functionality
    {  // before exiting overworld. Or use for an autosave, etc. NOT during a battlescene.
       
           SavePlayerPartnerBasicData();
        SaveSharedPartnerData();
        SavePlayerInventoryContents();
        SaveWeaponItems();
        SavePlayerPartnerLocation();
        SaveLastActivePartner();
        SaveNPCData();
        PopUpUI();
      
    }
    public void LoadGlobalData()// return to overworld or load game from menu.
    {
        
       // LoadCurrentScene();
        LoadPlayerPartnerBasicData();
        LoadSharedPartnerData();
        LoadPlayerInventoryContents();
        LoadWeaponItems();
        LoadPlayerPartnerLocation();
        LoadLastActivePartner();
        LoadChosenPlayerAndPartner();
        LoadNPCData();
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
        SaveWeaponItems();
        SaveNPCData();

    }
    public void LoadBattleArenaData() //This will be called right before entering the arena, to persist data needed for the battle such as stats
    {
        LoadPlayerPartnerBasicData();
        LoadSharedPartnerData();
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
    void SaveCurrentScene(Scene scene)
    {
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
        ES3.Load("playerDataSO");
        ES3.Load("partner1BasicData", partner1SOData);
        ES3.Load("partner2BasicData", partner2SOData);
        ES3.Load("partner3BasicData", partner3SOData);
    }
    void SaveSharedPartnerData() //AKA PlayerData
    {
        ES3.Save("sharedPartnerData", sharedPartnerData);

    }
    void LoadSharedPartnerData()
    {
        if(ES3.KeyExists("sharedPartnerData"))
        ES3.Load("sharedPartnerData");
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
        playerWeapons = weaponInventoryManager.playerWeaponsInInventory;
        partnerWeapons = weaponInventoryManager.partnerWeaponsInInventory;

        ES3.Save("playerWeapons", playerWeapons);
        ES3.Save("partnerWeapons", partnerWeapons);
    }
    void LoadWeaponItems()
    {
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
     void SavePlayerPartnerLocation()
    {
        savedLocations = player.transform.position; 
      
        ES3.Save("playerPartnerLocation", savedLocations);

    }
    void LoadPlayerPartnerLocation()
    {
        if (ES3.KeyExists("playerPartnerLocation"))
        {
            savedLocations = ES3.Load<Vector2>("playerPartnerLocation");
            player.transform.position = savedLocations;
            partner.transform.position = savedLocations;
        }
        else
        {
            Debug.LogWarning("Key 'playerPartnerLocation' does not exist or data is not loaded.");

        }
    }
    void SaveLastActivePartner()
    {
        ES3.Save("lastActivePartner", activePartner);
    }
    void LoadLastActivePartner()
    {
        if (ES3.KeyExists("lastActivePartner"))
        {
            activePartner = ES3.Load<GameObject>("lastActivePartner");
            activePartner.SetActive(true);

            PartnerManager.Instance.SetLastPartnerActive(activePartner);
            
        }
        else
        {
            Debug.LogError("lastActivePartner key does not exist");
        }
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
