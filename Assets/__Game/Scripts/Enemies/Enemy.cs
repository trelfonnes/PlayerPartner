using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Dictionary<int, WeaponDataSO> MeleeWeaponDatas = new Dictionary<int, WeaponDataSO>();
    private Dictionary<int, WeaponDataSO> ProjectileWeaponDatas = new Dictionary<int, WeaponDataSO>();
    private int nextMeleeKey = 0;
    private int nextProjectileKey = 0;

    EnemyWeapon meleeWeapon;
    EnemyWeapon projectileWeapon;
    
    public EnemyMeleeAttackState MeleeState { get; private set; }
    public EnemyProjectileAttackState ProjectileState { get; private set; }
    public EnemyPlayerDetectedState PlayerDetectedState { get; private set; }

    
    public EnemyAttackState AttackState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyLowHealthState LowHealthState { get; private set; }
    
    public EnemyStateMachine StateMachine { get; private set;}
    public IEnemyMove moveStrategy { get; protected set; }
    public IEnemyLowHealth lowHealthStrategy { get; protected set; }
    public IEnemyMelee meleeStrategy { get; protected set; }
    public IEnemyProjectile projectileStrategy { get; protected set; }
    public CoreHandler core { get; private set;}
    public Animator anim { get; private set; }
    public EnemySOData enemySOData;
    public Dictionary<string, object> blackboard = new Dictionary<string, object>();
    public Vector2 enemyDirection;

    protected virtual void Awake()
    {
        StateMachine = new EnemyStateMachine();
        core = GetComponentInChildren<CoreHandler>();
        anim = GetComponent<Animator>();

        blackboard["EnemyData"] = enemySOData;
        meleeWeapon = transform.Find("MeleeAttack").GetComponent<EnemyWeapon>();
        projectileWeapon = transform.Find("ProjectileAttack").GetComponent<EnemyWeapon>();
        //can add blackboard data as needed if alternate from SO data
        // this can be useful for a single place to save enemy data if needed.
        // can be good for counting intervals for respawning etc. 
        IdleState = new EnemyIdleState(this, StateMachine, enemySOData, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, enemySOData, "move", moveStrategy);
        PatrolState = new EnemyPatrolState(this, StateMachine, enemySOData, "patrol");
        LowHealthState = new EnemyLowHealthState(this, StateMachine, enemySOData, "lowHealth", lowHealthStrategy);
        
         meleeWeapon.SetCore(core);
         projectileWeapon.SetCore(core);
        PlayerDetectedState = new EnemyPlayerDetectedState(this, StateMachine, enemySOData, "playerDetected");

        //Pass in the matching weapon script for the attack game object
        MeleeState = new EnemyMeleeAttackState(this, StateMachine, enemySOData, "attack", meleeWeapon, meleeStrategy, MeleeWeaponDatas);
        ProjectileState = new EnemyProjectileAttackState(this, StateMachine, enemySOData, "attack", projectileWeapon, projectileStrategy, ProjectileWeaponDatas);

    }

    //refactor to individual enemy class
    protected virtual void SetStrategies()
    {
      
    }
    protected virtual void Start()
    {
        StateMachine.Initialize(PatrolState);
        
    }
    protected virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        
    }
    protected virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    protected void SetMeleeWeaponDatas(List<WeaponDataSO> weaponDatas)
    {
        MeleeWeaponDatas.Clear(); // Clear the existing data in WeaponDatas

        foreach (WeaponDataSO weaponData in weaponDatas)
        {
            int key = GetNextMeleeKey(); // Get a unique key before setting it to key
            MeleeWeaponDatas[key] = weaponData;
        }
        foreach (var kvp in MeleeWeaponDatas)
        {
            Debug.Log($"Key: {kvp.Key}, WeaponData: {kvp.Value}");
        }
    }
    protected void SetProjectileWeaponDatas(List<WeaponDataSO> weaponDatas)
    {
        ProjectileWeaponDatas.Clear(); // Clear the existing data in WeaponDatas

        foreach (WeaponDataSO weaponData in weaponDatas)
        {
            int key = GetNextProjectileKey(); // Get a unique key before setting it to key
            ProjectileWeaponDatas[key] = weaponData;
        }
        foreach (var kvp in ProjectileWeaponDatas)
        {
            Debug.Log($"Key: {kvp.Key}, WeaponData: {kvp.Value}");
        }
    }
    private int GetNextMeleeKey()
    {
        return nextMeleeKey++; // Return the current key and increment for the next use

    }
    private int GetNextProjectileKey()
    {
        return nextProjectileKey++; // Return the current key and increment for the next use
    }
}
