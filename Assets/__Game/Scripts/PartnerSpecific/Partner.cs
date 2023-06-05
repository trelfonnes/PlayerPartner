using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : MonoBehaviour
{
    #region StateVariables for Specific Character

    public PlayerStateMachine StateMachine { get; private set; }
    public PartnerMoveState MoveState { get; private set; }
    public PartnerIdleState IdleState { get; private set; }
    public PartnerFollowIdleState FollowIdleState {get; private set;}
    public PartnerFollowMoveState FollowMoveState { get; private set; }

    #endregion
    public CoreHandler core { get; private set; }
    public Animator anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    protected PlayerData _playerData = new PlayerData(); //data for stats refactor might not need it here
    [SerializeField]
    protected PlayerSOData playerSOData;//Data for states  
    public Vector2 playerDirection;



    protected virtual void Awake()
    {
        
        core = GetComponentInChildren<CoreHandler>();
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine = new PlayerStateMachine();
        playerDirection = Vector2.down;
        MoveState = new PartnerMoveState(this, StateMachine, playerSOData, _playerData, "move");
        IdleState = new PartnerIdleState(this, StateMachine, playerSOData, _playerData, "idle");
        FollowIdleState = new PartnerFollowIdleState(this, StateMachine, playerSOData, _playerData, "followIdle");
        FollowMoveState = new PartnerFollowMoveState(this, StateMachine, playerSOData, _playerData, "followMove");
    }
    protected virtual void Start()
    {
        
      StateMachine.InitializePartner(FollowIdleState); //for when states are referenced in awake.
    }
    protected virtual void Update()
    {
       
        StateMachine.CurrentPartnerState.LogicUpdate();

    }
    protected virtual void FixedUpdate()
    {
        
        StateMachine.CurrentPartnerState.PhysicsUpdate();
    }

    #region For Saving Data BIND
    internal void Bind(PlayerData playerData)
    {
        _playerData = playerData;
        Debug.Log("binding");
    }
    #endregion
}
