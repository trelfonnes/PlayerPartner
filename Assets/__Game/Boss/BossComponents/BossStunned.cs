using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunned : BossCoreComponent
{
    bool isStunned;
    StunState currentStunState;
    private void Update()
    {
        if(currentStunState == StunState.active)
        {
            isStunned = true;
        }
        else
        {
            isStunned = false;
        }
        
    }

    public bool IsStunActive() //called by the decorator node 
    {
        return isStunned;
    }
    public void ChangeStunState(StunState state) // to be called by the fireLight tracker which will reference this class instance
    {
        currentStunState = state;
    }
   
}
public enum StunState
{
    idle,
    active,
    coolDown
}