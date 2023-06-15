using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Partner : MonoBehaviour
{
    #region StateVariables for Specific Character

    public PlayerStateMachine StateMachine { get; private set; }
    public PartnerMoveState MoveState { get; private set; }
    public PartnerIdleState IdleState { get; private set; }
    public PartnerFollowIdleState FollowIdleState {get; private set;}
    public PartnerFollowMoveState FollowMoveState { get; private set; }
    public PartnerEvolutionState EvolutionState { get; private set; }
    public PartnerDeEvolutionState DevolveState { get; private set; }
    #endregion
    public CoreHandler core { get; private set; }
    public Animator anim { get; private set; }
    public CinemachineVirtualCamera PartnerCamera
    {
        get { return partnerCamera; }
        set { partnerCamera = value; }
    }
    public PlayerInputHandler InputHandler { get; private set; }
    protected PlayerData _playerData; //data for stats refactor might not need it here
    [SerializeField]
    protected PlayerSOData playerSOData;//Data for states
    [SerializeField] public StatEvents statEvents;
    [SerializeField] CinemachineVirtualCamera partnerCamera;
    public Vector2 playerDirection;

 

    protected virtual void Awake()
    {
        
        core = GetComponentInChildren<CoreHandler>();
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        _playerData = PlayerData.Instance;
        StateMachine = new PlayerStateMachine();
        playerDirection = Vector2.down;
        MoveState = new PartnerMoveState(this, StateMachine, playerSOData, _playerData, "move");
        IdleState = new PartnerIdleState(this, StateMachine, playerSOData, _playerData, "idle");
        FollowIdleState = new PartnerFollowIdleState(this, StateMachine, playerSOData, _playerData, "followIdle");
        FollowMoveState = new PartnerFollowMoveState(this, StateMachine, playerSOData, _playerData, "followMove");
        EvolutionState = new PartnerEvolutionState(this, StateMachine, playerSOData, _playerData, "evolve");
        DevolveState = new PartnerDeEvolutionState(this, StateMachine, playerSOData, _playerData, "devolve");
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
    //TODO Implement a save system for writing SO Data
    internal void Bind(PlayerData playerData)
    {
        _playerData = playerData;
        Debug.Log("binding");
    }
    #endregion


}
