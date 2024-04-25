using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerJumpState : PartnerAbilityState
{
    int amountofJumpsLeft;
    Vector2 initialPosition;
    bool hasMovedJumpUnits = false;
    float jumpTimeout = .85f;
    float timer = 0f;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    public PartnerJumpState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
        amountofJumpsLeft = playerSOData.numberOfJumps;
    }

    public bool CanJump()
    {
        if(amountofJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DecreaseAmountOfJumpsLeft()
    {
        amountofJumpsLeft--;
    }
    public void ResetAmountOfJumpsLeft()
    {
        amountofJumpsLeft = playerSOData.numberOfJumps;
        hasMovedJumpUnits = false;
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Vector2 direction = new Vector2(partner.InputHandler.NormInputX, partner.InputHandler.NormInputY);
        initialPosition = partner.transform.position;
        playerSOData.Stamina -= 10f;
        DecreaseAmountOfJumpsLeft();
        Movement?.SetVelocityZero();
        CollisionSenses?.DisableHazardDetection();
        Debug.Log(direction);
        if (playerSOData.stage2 || playerSOData.stage3)
        {

           // statEvents.onCurrentEPZero += Devolve;
            Subscribe((handler) => statEvents.onCurrentEPZero += handler, Devolve);


        }
        Movement?.SetVelocity(direction * playerSOData.jumpForce);
        AudioManager.Instance.PlayAudioClip("Jump");

        timer = 0f;
        Subscribe((handler) => statEvents.onCurrentHealthZero += handler, Partner1Defeated);

    }

    public override void Exit()
    {
        base.Exit();
        ResetAmountOfJumpsLeft();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= Devolve;

        }
        statEvents.onCurrentHealthZero -= Partner1Defeated;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        statEvents.onCurrentEPZero -= Devolve;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float distance = Vector2.Distance(initialPosition, partner.transform.position);
        timer += Time.deltaTime;

        if (distance >= playerSOData.jumpDistance && !hasMovedJumpUnits)
        {
            hasMovedJumpUnits = true;
            Movement?.SetVelocityZero();
            CollisionSenses?.EnableHazardDetection();
            PSM.ChangePartnerState(partner.IdleState);
            partner.JumpCooldownTimer.Reset();

        }
        else if(timer >= jumpTimeout)
        {
            hasMovedJumpUnits = true;
            Movement?.SetVelocityZero();
            CollisionSenses?.EnableHazardDetection();
            PSM.ChangePartnerState(partner.IdleState);
            partner.JumpCooldownTimer.Reset();

        }
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    void Devolve()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
        isDevolvingAbilityCancel = true;
        if (!playerSOData.stage1)
        {
            Debug.Log("DEVOLVE YOU LITTLE SHITE");
            PSM.ChangePartnerState(partner.DevolveState);
        }
    }
}
