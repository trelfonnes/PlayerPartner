using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveLoadManager : GameManager
{
    [SerializeField] protected PlayerSOData playerSOData;
    [SerializeField] protected PlayerSOData partnerSOData;

    public static SaveLoadManager InstanceSLM { get; private set; }
   
    private void Awake()
    {
        if (InstanceSLM != null)
        {
            Destroy(gameObject);
            return;
        }
        InstanceSLM = this;
        DontDestroyOnLoad(gameObject);
    
    }
    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
        
    }
    public void SaveGame()
    {
        if (!IsSaveFile())
      {
            Debug.Log("Saved");
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/Player_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/Player_Data");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/Partner_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/Partner_Data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        //for Player
        FileStream filePlayer = File.Create(Application.persistentDataPath + "/game_save/Player_Data/Player_Data_SaveFile");
        //convert data here:
        var json = JsonUtility.ToJson(playerSOData);
        bf.Serialize(filePlayer, json);
        filePlayer.Close();
        //ForPartner
        FileStream filePartner = File.Create(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFile");
        var json2 = JsonUtility.ToJson(partnerSOData);
        bf.Serialize(filePartner, json2);
        filePartner.Close();
    }

    public void LoadGame()
    {
        Debug.Log("Hello");
    }



}
