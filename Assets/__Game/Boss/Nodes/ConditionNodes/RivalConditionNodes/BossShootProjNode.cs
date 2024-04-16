using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootProjNode : ConditionNode
{
    Timer timer;
    BossMeleeState meleeState;
   
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    public BossShootProjNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
        StartShootTimer();
        meleeState = BossMeleeState.idle;
    }
    void StartShootTimer()
    {
         timer = new Timer(blackboard.timeBetweenProj);
        
    }

    public override NodeState Execute()
    {
        timer.Update(Time.deltaTime);
        
        if (timer.IsFinished())
        {
           
                Debug.Log("Melee state from proj cond node" + meleeState);
                timer.Reset();
                return NodeState.success;
            
            
        }
        if (!timer.IsFinished())
        {
            return NodeState.failure;
        }
        else
        {
            return NodeState.failure;
        }
    }
}
