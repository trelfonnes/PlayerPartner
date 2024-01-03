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
    bool needsToInitializeStates;
    EnemyWeapon meleeWeapon;
    EnemyWeapon projectileWeapon;

    public static event Action<AreaType> onEnemyDefeated;
    [SerializeField] AreaType enemyAreaType;

    public EnemyMeleeAttackState MeleeState { get; private set; }
    public EnemyProjectileAttackState ProjectileState { get; private set; }
    public EnemyPlayerDetectedState PlayerDetectedState { get; private set; }

    
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDefeatedState DefeatedState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyLowHealthState LowHealthState { get; protected set; }
    public EnemyStunnedState StunnedState { get; private set; }
    public EnemyThinkState ThinkState { get; private set; }

    public EnemyStateMachine StateMachine { get; private set;}

    public IEnemyMove moveStrategy { get; protected set; }
    public IEnemyLowHealth lowHealthStrategy { get; protected set; }
    public IEnemyMelee meleeStrategy { get; protected set; }
    public IEnemyProjectile projectileStrategy { get; protected set; }
    public IEnemyItemSpawn itemSpawnStrategy { get; protected set; }

    public CoreHandler core { get; private set;}
    public Animator anim { get; private set; }
    public EnemySOData enemySOData;
    public EnemyData enemyData;
    public EnemyStatEvents statEvents;

    public Dictionary<string, object> blackboard = new Dictionary<string, object>();
    public Vector2 enemyDirection;

    Transform itemSpawnPoint;

    protected virtual void Awake()
    {
        StateMachine = new EnemyStateMachine();
        core = GetComponentInChildren<CoreHandler>();
        anim = GetComponent<Animator>();
        itemSpawnPoint = GetComponent<Transform>();
        enemyData = new EnemyData(enemySOData);
        blackboard["EnemyData"] = enemySOData;
        meleeWeapon = transform.Find("MeleeAttack").GetComponent<EnemyWeapon>();
        projectileWeapon = transform.Find("ProjectileAttack").GetComponent<EnemyWeapon>();
        
        //can add blackboard data as needed if alternate from SO data
        // this can be useful for a single place to save enemy data if needed.
        // can be good for counting intervals for respawning etc. 
        IdleState = new EnemyIdleState(this, StateMachine, enemySOData, enemyData, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, enemySOData, enemyData, "move", moveStrategy);
        PatrolState = new EnemyPatrolState(this, StateMachine, enemySOData, enemyData, "patrol");
        LowHealthState = new EnemyLowHealthState(this, StateMachine, enemySOData, enemyData, "lowHealth", lowHealthStrategy);
        ThinkState = new EnemyThinkState(this, StateMachine, enemySOData, enemyData, "think");
        DefeatedState = new EnemyDefeatedState(this, StateMachine, enemySOData, enemyData, "defeated", itemSpawnStrategy, itemSpawnPoint);
        StunnedState = new EnemyStunnedState(this, StateMachine, enemySOData, enemyData, "stunned");
         meleeWeapon.SetCore(core);
         projectileWeapon.SetCore(core);
        PlayerDetectedState = new EnemyPlayerDetectedState(this, StateMachine, enemySOData, enemyData, "playerDetected");

        //Pass in the matching weapon script for the attack game object
        MeleeState = new EnemyMeleeAttackState(this, StateMachine, enemySOData, enemyData, "attack", meleeWeapon, meleeStrategy, MeleeWeaponDatas);
        ProjectileState = new EnemyProjectileAttackState(this, StateMachine, enemySOData, enemyData, "attack", projectileWeapon, projectileStrategy, ProjectileWeaponDatas);

    }
    private void OnDisable()
    {
    }
    //refactor to individual enemy class
    protected virtual void SetStrategies()
    {
      
    }
    public void InitializeAreaType(AreaType areaType)
    {
        enemyAreaType = areaType;
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
        
    }
    protected void SetProjectileWeaponDatas(List<WeaponDataSO> weaponDatas)
    {
        ProjectileWeaponDatas.Clear(); // Clear the existing data in WeaponDatas

        foreach (WeaponDataSO weaponData in weaponDatas)
        {
            int key = GetNextProjectileKey(); // Get a unique key before setting it to key
            ProjectileWeaponDatas[key] = weaponData;
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
    private void OnEnable()
    {
        enemyData.ResetData();
        if (needsToInitializeStates)
        {
            StateMachine.Initialize(PatrolState);

        }

    }
    public void TurnEnemyOFF()
    {
        StateMachine.ChangeState(PatrolState);
        enemyData.ResetData();
        PlayerData.Instance.GainExperience(enemySOData.expYield);
        onEnemyDefeated?.Invoke(enemyAreaType);
        needsToInitializeStates = true;
        StateMachine.CurrentState.OnDisable();

        gameObject.SetActive(false);
    }
}
