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
        SelectorNode rootNode = new SelectorNode(componentLocator, blackboard,
            new SequenceNode(componentLocator, blackboard,
                new BossDefeatedConditionNode(blackboard, componentLocator),
                new ArenaDefeatedActionNode(blackboard, componentLocator, "defeated")),
            new SequenceNode(componentLocator, blackboard,
                new FatiguedConditionNode(blackboard, componentLocator),
                new RestActionNode(blackboard, componentLocator, "rest")),
            new SequenceNode(componentLocator, blackboard,
                new PlayerDetectionNode(blackboard, componentLocator),
                new DirectionalMeleeActionNode(blackboard, componentLocator, "attack")),
            new SequenceNode(componentLocator, blackboard,
                new BossShootProjNode(blackboard, componentLocator),
                new DirectionalProjActionNode(blackboard, componentLocator, "attack")),
            new SequenceNode(componentLocator, blackboard,
                new MovementTypeDecoratorNode(blackboard, componentLocator,
                    new BossChargeActionNode(blackboard, componentLocator, "move"),
                    new BossDistanceActionNode(blackboard, componentLocator, "move")))
            );

        SetBehaviorTreeRoot(rootNode);
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
        blackboard.stamina = bossStats.stamina;
        blackboard.restTime = bossStats.restTime;
        blackboard.timeBetweenProj = bossStats.timeBetweenProjectiles;
        blackboard.chargeBuffer = bossStats.chargeBuffer;
        blackboard.distancingLength = bossStats.distancingLength;
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
