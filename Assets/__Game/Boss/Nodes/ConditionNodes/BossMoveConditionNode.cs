using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveConditionNode : ConditionNode
{
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;

    public BossMoveConditionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }


    public override NodeState Execute()
    {
        Debug.Log("Hello world from moveConditionNode");
        if (Collisions.MovePointCheck)
        {
            Debug.Log("this is within BossMoveConditionNode" + Collisions.MovePointCheck);

            // find a new move point
            return NodeState.success;// a move point has been detected, pick a new one/ run pick movePosition Node
        }

        else
        {
           // blackboard.chooseDirection = true;
            return NodeState.failure; //a movePoint was not reached, stay in same position
        }
        // order = movement, move condition, change movePoint.
        
    }
}
