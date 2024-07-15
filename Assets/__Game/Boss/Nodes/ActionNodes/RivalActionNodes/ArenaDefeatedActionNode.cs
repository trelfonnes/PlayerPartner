using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDefeatedActionNode : ActionNode
{
    bool alreadyTriggered; // this node only needs to fire its logic once, as the enemy will be dead
    private BossDefeated Defeated { get => defeated ?? componentLocator.GetCoreComponent(ref defeated); }
    private BossDefeated defeated;
    public ArenaDefeatedActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {
        alreadyTriggered = false; //initialize as false
    }

    public override NodeState Execute()
    {
        if (!alreadyTriggered)
        {
            SetAnimation();
            Defeated.ArenaEnemyDefeated(); //only called to trigger event once. Needed system/level components will listen.
            alreadyTriggered = true;
            return NodeState.success;
        }
        return NodeState.success;
    }

    public override void SetAnimation()
    {
        base.SetAnimation(); //opponent defeated sprite plays and does not loop, remaining in this state.
    }
}
