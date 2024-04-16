using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalOne : BossAI
{
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions);}
    private BossCollisionDetection collisions;

    [SerializeField] EnemyStatEvents bossStatEvents;
    [SerializeField] Animator anim;
    bool battleStarted;

    [SerializeField] float decisionTimer = 3f;


    protected override void Start()
    {
        base.Start();
        timer = new Timer(decisionTimer);
        blackboard = new BossBlackboard();
        componentLocator = GetComponentInChildren<BossComponentLocator>();
        anim = GetComponent<Animator>();
        InitializeStats();
        InitializeBehaviorTree();
    }

    protected override void InitializeBehaviorTree()
    {
        base.InitializeBehaviorTree();
        //all nodes go here in order

    }
    protected override void Update()
    {
        base.Update();
        // if health is above 25% do behavior tree root node, else, switch to second stage root
        if (battleStarted)
        {
            behaviorTreeFirstStageRoot.Execute();
            if (blackboard.isLowHealth)
            {
                
                // switch to next behavior tree strategy if applicable
            }

        }
    }

    void InitializeStats() // set the individual boss specs to the blackboard
    {
        blackboard.anim = anim;
        blackboard.moveSpeed = bossStats.moveSpeed;
        blackboard.meleeTime = bossStats.meleeTime;
        blackboard.timeBetweenProj = bossStats.timeBetweenProjectiles;
        blackboard.projectileType = bossStats.projectileType;
       // int startingMovePoint = Random.Range(0, 5);
      //  blackboard.moveDirection = Collisions.MovePoints[startingMovePoint].position;
       // Collisions.lastMovePoint = Collisions.MovePoints[startingMovePoint];
    }
    void SetBehaviorTreeRoot(BehaviorNode root)
    {
        behaviorTreeFirstStageRoot = root;
    }
    void SetSecondStageRoot(BehaviorNode secondRoot)
    {
        behaviorTreeSecondStageRoot = secondRoot;
    }
    void SetThirdStageRoot(BehaviorNode thirdRoot)
    {
        behaviorTreeThirdStageRoot = thirdRoot;
    }

    protected override void OnEnable()
    {
        base.OnDisable();

        bossStatEvents.onHealthZero += HealthZero;
        bossStatEvents.onHealthLow += HealthLow;
        bossStatEvents.onBattleStart += BattleStarted;

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        bossStatEvents.onHealthZero -= HealthZero;
        bossStatEvents.onHealthLow -= HealthLow;
        bossStatEvents.onBattleStart -= BattleStarted;


    }
    void BattleStarted()
    {
        battleStarted = true;
    }
    private void HealthLow()
    {
        bossStats.isLowHealth = true;
        blackboard.isLowHealth = true;
    }

    private void HealthZero()
    {
        bossStats.isDefeated = true;
        blackboard.isDefeated = true;
    }
}
