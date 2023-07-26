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

    [SerializeField] GameObject gameSavedUI;

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
    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
        
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
