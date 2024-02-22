using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] WeaponAutoGenerator thisWeaponsAutoGenerator;

    public event Action onEnter;
    public event Action onExit;
    Animator anim;
    public AnimationEventHandler EventHandler { get; private set; }
    public GameObject BaseGO { get; private set; } // set these in the inspector
    public GameObject WeaponSpriteGO { get; private set; }
    public BossComponentLocator ComponentLocator;
    public WeaponDataSO weaponData { get; private set; }
    int currentAttackCounter;
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= weaponData.NumberOfAttacks ? 0 : value;
    }

    private void Awake()
    {   //Coment these in if can't set in the inspector.
        BaseGO = transform.Find("Base").gameObject;
        WeaponSpriteGO = transform.Find("WeaponSprite").gameObject;

        anim = BaseGO.GetComponent<Animator>();
        EventHandler = BaseGO.GetComponent<AnimationEventHandler>();
      
    }

    public void Enter()
    {
        // set any anim parameters
        onEnter?.Invoke();
    }
    public void SetComponentLocator(BossComponentLocator componentLocator)
    {
        ComponentLocator = componentLocator;
    }
    public void SetInitialWeaponData(WeaponDataSO data)
    {
        weaponData = data;
    }
    public void SetNewWeaponData(WeaponDataSO data)
    {
        weaponData = data;
        thisWeaponsAutoGenerator.GenerateWeapon(weaponData);
    }
    void Exit()
    {
        anim.SetBool("active", false);
        onExit?.Invoke();
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
