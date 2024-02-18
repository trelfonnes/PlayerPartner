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
             new SequenceNode(componentLocator, blackboard,
             new BossMovementNode(blackboard, componentLocator),
             new BossMoveConditionNode(blackboard, componentLocator), // condition for movement comes first
             new BossPickMovePoint(blackboard, componentLocator)));
            // selector node for attacks next.

        //list of action, decorator, or condition node in this sequence) 

        //new SequenceNode(componentLocator, blackboard,
        // list of action decorator or condition node in this sequence
        // new CheckHealthNode(Potential dependencies),
        // new SummonMinionsNode(Potential Dependencies),
        SetBehaviorTreeRoot(rootNode);
    }
    protected override void Update()
    {
        base.Update();
        behaviorTreeRoot.Execute();
    }
    void InitializeStats()
    {
        blackboard.moveSpeed = bossStats.moveSpeed;
        
        blackboard.moveDirection = Collisions.MovePoints[2].position;
        Collisions.lastMovePoint = Collisions.MovePoints[2];
    }
    void SetBehaviorTreeRoot(BehaviorNode root)
    {
        behaviorTreeRoot = root;
    }
}
