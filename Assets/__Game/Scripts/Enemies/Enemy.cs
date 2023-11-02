using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyAttackState AttackState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }

    public EnemyStateMachine StateMachine { get; private set;}

    public CoreHandler core { get; private set;}
    public Animator anim { get; private set; }
    public EnemySOData enemySOData;
    public Dictionary<string, object> blackboard = new Dictionary<string, object>();

    protected virtual void Awake()
    {
        StateMachine = new EnemyStateMachine();
        core = GetComponentInChildren<CoreHandler>();
        blackboard["EnemyData"] = enemySOData;
        //can add blackboard data as needed if alternate from SO data
        // this can be useful for a single place to save enemy data if needed.
        // can be good for counting intervals for respawning etc. 
        IdleState = new EnemyIdleState(this, StateMachine, enemySOData, "idle");
        MoveState = new EnemyMoveState(this, StateMachine, enemySOData, "move");
        AttackState = new EnemyAttackState(this, StateMachine, enemySOData, "attack");
        PatrolState = new EnemyPatrolState(this, StateMachine, enemySOData, "patrol");


    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        //StateMachine.Initialize();
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        
    }
    protected virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
}
