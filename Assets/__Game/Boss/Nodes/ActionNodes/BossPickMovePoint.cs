using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPickMovePoint : ActionNode
{
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    public BossPickMovePoint(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
      
    }

    public override NodeState Execute()
    {
        Debug.Log("singleExecute" + singleExecute);

        
            Transform targetMovePoint = Collisions.GetRandomMovePoint();
            blackboard.moveDirection = targetMovePoint.position;
            Debug.Log("Inside pick new pos" + targetMovePoint.position);
            blackboard.chooseDirection = false;
            return NodeState.success;
        
        


     
    } 
}
