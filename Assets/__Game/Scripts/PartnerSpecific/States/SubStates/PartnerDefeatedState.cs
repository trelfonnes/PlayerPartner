using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerDefeatedState : PartnerFollowState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    public PartnerDefeatedState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        partner.partnerCollider.enabled = false;
        partner.statEvents.onPartnerFullyRestored += PartnerRevived;
        partner.evolutionEvents.SwitchToPlayer();
        PlayerData.Instance.partnerIsDefeated = true;
    }

    public override void Exit()
    {
        base.Exit();
        partner.partnerCollider.enabled = true;
        partner.statEvents.onPartnerFullyRestored -= PartnerRevived;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player != null)
        {
            var direction = (player.position - partner.transform.position).normalized;
            Movement?.CheckIfShouldFlipFollowing(direction);

            if (!isTouchingPlayer)
            {
                partner.transform.position += direction * playerSOData.followSpeed * Time.deltaTime;



            }
            if (direction.x != 0 && direction.y != 0)
            {
                partner.anim.SetFloat("moveY", Mathf.Round(direction.y));
                partner.anim.SetFloat("moveX", Mathf.Round(direction.x));
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void PartnerRevived()
    {
        PSM.ChangePartnerState(partner.FollowIdleState);
    }
}
