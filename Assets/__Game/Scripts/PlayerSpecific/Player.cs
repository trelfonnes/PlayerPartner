using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region StateVariables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }


    #endregion
    
    #region Components on Player GO

    public CoreHandler core { get; private set; }
    [SerializeField] private PlayerBasicData playerData;//Data for states add data for stats
    public Animator anim { get; private set; }
    //this script accesses the playerstate script and its functions through this, which has a reference to it called CurrentState
    public PlayerInputHandler InputHandler { get; private set; }
    #endregion

    #region Unity Callback Functions Initialized in Awake Method

    private void Awake()
    {
        core = GetComponentInChildren<CoreHandler>();
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }
    #endregion


    void Start()
    {
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine.Initialize(IdleState); //for when states are referenced in awake.
    }

    void Update()
    {
        //connect with logic update in playerstate
        StateMachine.CurrentState.LogicUpdate();
        
    }

    private void FixedUpdate()
    {
        //connect physics update
        StateMachine.CurrentState.PhysicsUpdate();
    }
}
