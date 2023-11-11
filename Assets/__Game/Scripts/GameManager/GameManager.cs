using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : DataReferenceInheritor
{

    public GameObject chosenPartnerCharacter;
    public GameObject chosenPlayerCharacter;// these are for spawning relations on scene switching
    public GameObject[] playablePartnerPrefabs; // all creatures
    public GameObject[] playablePlayerPrefabs; // male and female
    int chosenPartnerCharacterIndex = 0;
    int chosenPlayerCharacterIndex = 0;

    public ItemsObjectPool objectPool;
    public static GameManager Instance { get; private set; }
    IItemSpawnStrategy extraRareStrategy;
    IItemSpawnStrategy rareStrategy;
    IItemSpawnStrategy regularStrategy;

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




    }



    private void Start()
    {
        regularStrategy = new RegularItemSpawnStrategy(objectPool);
        rareStrategy = new RareItemSpawnStrategy(objectPool);
        extraRareStrategy = new ExtraRareItemSpawnStrategy(objectPool);

        // Set the desired strategy in the ItemSpawnSystem (you can do this based on game logic).
        // For example, based on the category of defeated enemy or broken object, you can set the strategy.
        ItemSpawnSystem.Instance.SetInitialItemSpawnStrategy(regularStrategy);
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

    public void SetChosenCharacter(int PartnercharacterIndex, int PlayerCharacterIndex)
    {
        chosenPartnerCharacterIndex = PartnercharacterIndex;
        chosenPlayerCharacterIndex = PlayerCharacterIndex;
    }

    public GameObject GetChosenPartnerPrefab()
    {
        return playablePartnerPrefabs[chosenPartnerCharacterIndex];
    }
    public GameObject GetChosenPlayerPrefab()
    {
        return playablePlayerPrefabs[chosenPlayerCharacterIndex];
    }

    //TODO: This will be the activate function found in the player and partner class
    // bool isChosenPlayer =GameManager.Instance.GetChosenPartnerPrefab() == gameObject;
    // bool isChosenPlayer = GameManager.Instance.GetChosenPlayerPrefab() == gameObject;
    //gameObject.SetActive(isChosenPlayer);
    
    public void LoadNewScene(string sceneName, GameObject player) //call this from collider to enter next level and pass in itself and desired scene.
    {
        SavePlayerPosition(player);
        SceneManager.LoadScene(sceneName);
    }
    void SavePlayerPosition(GameObject player)//for saving previous scenes position
    {
        Vector3 playerPosition = player.transform.position;
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

}
