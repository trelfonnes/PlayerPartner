using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    
    //this script accesses the playerstate script and its functions through this, which has a reference to it called CurrentState
    public PlayerStateMachine StateMachine { get; private set;} 

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        //CurrentState.Initialize(IdleState); //for when states are referenced in awake.
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
