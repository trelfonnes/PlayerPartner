using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] WeaponAutoGenerator thisWeaponsAutoGenerator;
    public GameObject BaseGO { get; private set; }
    public GameObject WeaponSpriteGO { get; private set; }
   Enemy enemy;
    public AnimationEventHandler EventHandler { get; private set; }
    public CoreHandler Core { get; private set; }
    public event Action onExit;
    public event Action onEnter;
    public event Action<bool> OncurrentInputChange;
    Animator anim;
    public WeaponDataSO weaponData { get; private set; }
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= weaponData.NumberOfAttacks ? 0 : value;

    }
    int currentAttackCounter;

    private void Awake()
    {
        BaseGO = transform.Find("Base").gameObject;
        WeaponSpriteGO = transform.Find("WeaponSprite").gameObject; // If this is too non performant I can serialize and drag in game objects.
        enemy = GetComponentInParent<Enemy>();
        anim = BaseGO.GetComponent<Animator>();
        EventHandler = BaseGO.GetComponent<AnimationEventHandler>();
    }
    public void Enter()
    {
        Debug.Log("Enter weapon Set anim to True");
        anim.SetBool("active", true);
        anim.SetFloat("moveX", enemy.enemyDirection.x); //might need to store "latest direction" like I did with player
        anim.SetFloat("moveY", enemy.enemyDirection.y);
        onEnter?.Invoke();
    }
    public void SetCore(CoreHandler core)
    {
        Core = core;
    }
    public void SetInitialEnemyData(WeaponDataSO data)
    {
        weaponData = data;
    }
    public void SetNewEnemyData(WeaponDataSO data)
    {
        weaponData = data;
        thisWeaponsAutoGenerator.GenerateWeapon(weaponData); //taking place of the inventory. works via the concrete interface

    }
    void Exit()
    {
        Debug.Log("Exit weapon Set anim to false");

        anim.SetBool("active", false);
        onExit.Invoke();
    }
    private void OnEnable()
    {
        EventHandler.OnFinish += Exit;
    }

    private void OnDisable()
    {
        EventHandler.OnFinish -= Exit;
    }
}
