using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : BossAI
{
    [SerializeField] float decisionTimer = 3f;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    [SerializeField] EnemyStatEvents bossStatEvents;
    [SerializeField] Animator anim;
    bool battleStarted;

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

        SelectorNode rootNode = new SelectorNode(componentLocator, blackboard,
              //execute behavior with decorator moving, melee, or stun
              new SequenceNode(componentLocator, blackboard,
                new BossDefeatedConditionNode(blackboard, componentLocator),
                new BossDefeatedActionNode(blackboard, componentLocator, "defeated")),

              //check when to move to new pos
              new SequenceNode(componentLocator, blackboard,
                new BossMoveConditionNode(blackboard, componentLocator), // condition for movement comes first
                new BossPickMovePoint(blackboard, componentLocator, "move")),
              //check when to shoot a projectile
              new SequenceNode(componentLocator, blackboard,
                new BossShootProjNode(blackboard, componentLocator),
                new BossProjActionNode(blackboard, componentLocator, "move")),
              new SequenceNode(componentLocator, blackboard,
                new ConditionalExecutionDecorator(blackboard, componentLocator,
                    new BossStunActionNode(blackboard, componentLocator, "stun"),
                    new BossMeleeActionNode(blackboard, componentLocator, "attack"),
                    new BossMovementNode(blackboard, componentLocator, "move")))
              

             ) ;
            // selector node for attacks next.

        //list of action, decorator, or condition node in this sequence) 

        //new SequenceNode(componentLocator, blackboard,
        // list of action decorator or condition node in this sequence
        // new CheckHealthNode(Potential dependencies),
        // new SummonMinionsNode(Potential Dependencies),
        SetBehaviorTreeRoot(rootNode);
        //set next stage roots when applicable
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
                blackboard.moveSpeed = 7f;
                blackboard.timeBetweenProj = 2f;
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
        int startingMovePoint = Random.Range(0, 5);
        blackboard.moveDirection = Collisions.MovePoints[startingMovePoint].position;
        Collisions.lastMovePoint = Collisions.MovePoints[startingMovePoint];
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
        base.OnEnable();
    
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
