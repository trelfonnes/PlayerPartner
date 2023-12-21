using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvolutionState : PlayerBasicState
{
    protected PlayerCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private PlayerCollisionSenses collisionSenses;
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
     //Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); } //Service locator pattern not working here
    private Movement movement;

    public PlayerEvolutionState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }

   

    public override void Enter()
    {
        base.Enter();
        player.evolutionEvents.OnReturnFromEvolution += StopEvolution;
    }

    public override void Exit()
    {
        base.Exit();
        player.evolutionEvents.OnReturnFromEvolution -= StopEvolution;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityZero();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    void StopEvolution()
    {
        if (player)
        {
            PSM.ChangeState(player.IdleState);
        }
    }
}
