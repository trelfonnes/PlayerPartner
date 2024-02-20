using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : BossAI
{
    [SerializeField] float decisionTimer = 3f;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    protected override void Start()
    {
        base.Start();
        timer = new Timer(decisionTimer);
        blackboard = new BossBlackboard();
        componentLocator = GetComponentInChildren<BossComponentLocator>();
        InitializeStats();
        InitializeBehaviorTree();
    }
    protected override void InitializeBehaviorTree()
    {
        base.InitializeBehaviorTree();

        SelectorNode rootNode = new SelectorNode(componentLocator, blackboard,
            // for movement 
            new SequenceNode(componentLocator, blackboard,
                new BossMovementNode(blackboard, componentLocator),
                new BossMoveConditionNode(blackboard, componentLocator), // condition for movement comes first
                new BossPickMovePoint(blackboard, componentLocator)),
                //for projectiles             
             new SequenceNode(componentLocator, blackboard,
                new BossShootProjNode(blackboard, componentLocator),
                new BossProjActionNode(blackboard, componentLocator)),
             // for melee
             new SequenceNode(componentLocator, blackboard,
                new BossMeleeConditionNode(blackboard, componentLocator),
                new BossMeleeActionNode(blackboard, componentLocator))
             );
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

        behaviorTreeFirstStageRoot.Execute();
    }
    void InitializeStats() // set the individual boss specs to the blackboard
    {
        blackboard.moveSpeed = bossStats.moveSpeed;
        blackboard.meleeTime = bossStats.meleeTime;
        blackboard.timeBetweenProj = bossStats.timeBetweenProjectiles;
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
}
