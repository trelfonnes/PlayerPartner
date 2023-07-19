using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveLoadManager : DataReferenceInheritor
{
  

    public static SaveLoadManager InstanceSLM { get; private set; }
   
    protected override void Awake()
    {
        base.Awake();
       

        

        
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
        FileStream filePlayer = File.Create(Application.persistentDataPath + "/game_save/Player_Data/Player_Data_SaveFilePlayer");
        //convert data here:
        var json = JsonUtility.ToJson(playerSOData);
        bf.Serialize(filePlayer, json);
        filePlayer.Close();
        //ForPartner
        FileStream filePartner1 = File.Create(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner1");
        var json2 = JsonUtility.ToJson(partner1SOData);
        bf.Serialize(filePartner1, json2);
        filePartner1.Close();
        FileStream filePartner2 = File.Create(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner2");
        var json3 = JsonUtility.ToJson(partner2SOData);
        bf.Serialize(filePartner1, json2);
        filePartner1.Close(); 
        FileStream filePartner3 = File.Create(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner3");
        var json4 = JsonUtility.ToJson(partner3SOData);
        bf.Serialize(filePartner1, json2);
        filePartner1.Close();

    }

    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/Player_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/Player_Data");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/Partner_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/Partner_Data");
        }
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/Player_Data/Player_Data_SaveFilePlayer"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/Player_Data/Player_Data_SaveFilePlayer", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerSOData);
            file.Close();
        }
        if (File.Exists(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner1"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner1", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), partner1SOData);
            file.Close();

        }
        if (File.Exists(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner2"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner2", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), partner2SOData);
            file.Close();

        }
        if (File.Exists(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner3"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/Partner_Data/Partner_Data_SaveFilePartner3", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), partner3SOData);
            file.Close();

        }

    }



}
