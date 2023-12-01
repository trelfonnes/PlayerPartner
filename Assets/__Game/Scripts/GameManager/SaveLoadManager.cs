using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveLoadManager : DataReferenceInheritor
{
    PlayerSOData playerBasicData;
    PlayerSOData partner1BasicData;
    PlayerSOData partner2BasicData;
    PlayerSOData partner3BasicData;
    [SerializeField] PlayerData sharedPartnerData;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] List<inventoryItems> itemsAmountInInventory;
    [SerializeField] GameObject gameSavedUI;
    [SerializeField] WeaponInventoryManager weaponInventoryManager;
   [SerializeField] List<WeaponInventoryItemSO> playerWeapons;
   [SerializeField] List<WeaponInventoryItemSO> partnerWeapons;
    [SerializeField] Player player;
    [SerializeField] Partner partner;
    [SerializeField] Vector2 savedLocations;

    [SerializeField] GameObject activePartner;

    public static SaveLoadManager Instance;
    

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
    public void SaveWithEasySave()
    {
        SavePlayerPartnerBasicData();
        SaveSharedPartnerData();
        SavePlayerInventoryContents();
        SaveWeaponItems();
        SavePlayerPartnerLocation();
        SaveLastActivePartner();
        PopUpUI();
    }
    public void LoadWithEasySave()
    {
        LoadPlayerPartnerBasicData();
        LoadSharedPartnerData();
        LoadPlayerInventoryContents();
        LoadWeaponItems();
        LoadPlayerPartnerLocation();
        LoadLastActivePartner();
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
        ES3.Load("sharedPartnerData");
    }
    void SavePlayerInventoryContents()
    {
        ES3.Save("playerInventory", playerInventory);
        ES3.Save("individualInventoryItems", itemsAmountInInventory);
    } 
   
    void LoadPlayerInventoryContents()
    {
        ES3.Load("playerInventory");
        ES3.Load("individualInventoryItems");
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
