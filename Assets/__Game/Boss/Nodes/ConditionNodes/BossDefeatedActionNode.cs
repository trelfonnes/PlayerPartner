using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeatedActionNode : ActionNode
{
    private BossParticleFX ParticleFX { get => particleFX ?? componentLocator.GetCoreComponent(ref particleFX); }
    private BossParticleFX particleFX;

    private BossDefeated BossDefeated { get => bossDefeated ?? componentLocator.GetCoreComponent(ref bossDefeated); }
    private BossDefeated bossDefeated;

    public BossDefeatedActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {

    }

    public override NodeState Execute()
    {
        BossDefeated.Defeated();
        return NodeState.success;
    }
    public override void SetAnimation()
    {
        base.SetAnimation();
    }
}
