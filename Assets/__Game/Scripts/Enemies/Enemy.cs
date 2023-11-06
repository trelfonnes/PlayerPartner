using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyAttackState AttackState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyPlayerDetectedState PlayerDetectedState { get; private set; }
    public EnemyLowHealthState LowHealthState { get; private set; }
    
    public EnemyStateMachine StateMachine { get; private set;}
    public IEnemyMove moveStrategy { get; protected set; }
    public IEnemyLowHealth lowHealthStrategy { get; protected set; }
    public CoreHandler core { get; private set;}
    public Animator anim { get; private set; }
    public EnemySOData enemySOData;
    public Dictionary<string, object> blackboard = new Dictionary<string, object>();
    public Vector2 enemyDirection;
    protected virtual void Awake()
    {
        StateMachine = new EnemyStateMachine();
        core = GetComponentInChildren<CoreHandler>();
        blackboard["EnemyData"] = enemySOData;
        //can add blackboard data as needed if alternate from SO data
        // this can be useful for a single place to save enemy data if needed.
        // can be good for counting intervals for respawning etc. 
        IdleState = new EnemyIdleState(this, StateMachine, enemySOData, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, enemySOData, "move", moveStrategy);
        AttackState = new EnemyAttackState(this, StateMachine, enemySOData, "attack");
        PatrolState = new EnemyPatrolState(this, StateMachine, enemySOData, "patrol");
        PlayerDetectedState = new EnemyPlayerDetectedState(this, StateMachine, enemySOData, "playerDetected");
        LowHealthState = new EnemyLowHealthState(this, StateMachine, enemySOData, "lowHealth", lowHealthStrategy);
    }

    //refactor to individual enemy class
    protected virtual void SetStrategies()
    {
      
    }
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
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
}
