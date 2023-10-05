using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : DataReferenceInheritor
{

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


}
