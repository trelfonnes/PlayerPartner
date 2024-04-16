using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDefeatedActionNode : ActionNode
{
    public ArenaDefeatedActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {
    }

    public override NodeState Execute()
    {
        //TODO: Implement calling the defeated component. It will have access to communicating to the scene what to do.
        throw new System.NotImplementedException();
    }

    public override void SetAnimation()
    {
        base.SetAnimation(); //opponent defeated sprite plays.
    }
}
