using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState CurrentState { get; private set; }
    public PartnerState CurrentPartnerState { get; private set; }
    //passed in from the referenced states in the player script
    public void Initialize(PlayerState StartingState) 
    {
        CurrentState = StartingState;
        CurrentState.Enter();
    }
    public void InitializePartner(PartnerState StartingState)
    {
        CurrentPartnerState = StartingState;
        CurrentPartnerState.Enter();

    }
    //called when state needs changed in individual states through
    ////the inherited variable PSM in PlayerState
    public void ChangeState(PlayerState NewState) 
    {
        CurrentState.Exit();
        CurrentState = NewState;
        CurrentState.Enter();
    }
    public void ChangePartnerState(PartnerState NewState)
    {
        CurrentPartnerState.Exit();
        CurrentPartnerState = NewState;
        CurrentPartnerState.Enter();
    }
   

}
