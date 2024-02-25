using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalExecutionDecorator : DecoratorNode
{
    private BossMelee Melee { get => melee ?? componentLocator.GetCoreComponent(ref melee); }
    private BossMelee melee;
    private BossStunned Stunned { get => stunned ?? componentLocator.GetCoreComponent(ref stunned); }
    private BossStunned stunned;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private readonly List<BehaviorNode> nodeChildren;

    public ConditionalExecutionDecorator(BossBlackboard blackboard, BossComponentLocator componentLocator, params BehaviorNode[] childNodes)
    { //make sure they are passed in at the right order
        this.nodeChildren = new List<BehaviorNode>(childNodes); 
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }
    public override NodeState Execute()
    {
        if (Stunned.IsStunActive())
        {
            //stop Movement in stun action node
            NodeState result = nodeChildren[0].Execute();
            return result;
        }
      

        if (Collisions.IsPlayerInFieldOfView) //or melee state is not idle
        {
           NodeState result = nodeChildren[1].Execute();
            return result;
        }
        else if (Melee.GetCurrentMeleeState() != BossMeleeState.idle)// still run bc it was triggered previously
        {
            NodeState result = nodeChildren[1].Execute();
           return result;
        }
        else // both are idle
        {
            NodeState result = nodeChildren[2].Execute();
            return result;
            // execute movement node.
        }
    }
}
