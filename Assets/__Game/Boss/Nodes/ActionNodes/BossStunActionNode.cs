using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunActionNode : ActionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private BossCombatReceiver CombatReceiver { get => combatReceiver ?? componentLocator.GetCoreComponent(ref combatReceiver); }
    private BossCombatReceiver combatReceiver;
    public BossStunActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {

    }

    public override NodeState Execute()
    {
        //Play the stunned animation and access the movement component to stop movement;
        Movement.StopMovement();
        CombatReceiver.TurnCombatColliderOn(true);
        // this will be exited via the decorator reading the stun component
        SetAnimation();
        return NodeState.success;
    }
    public override void SetAnimation()
    {
       
        base.SetAnimation();
        Debug.Log("SetStun Animation " + animBoolName);
    }
}
