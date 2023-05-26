using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicState : PlayerState
{
    protected int yInput;
    protected int xInput;


public PlayerBasicState(Player player, PlayerStateMachine PSM, PlayerBasicData playerData, string animBoolName) : base(player, PSM, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        // TODO add references to collision senses
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
