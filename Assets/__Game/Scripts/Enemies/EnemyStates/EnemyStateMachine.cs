using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState CurrentState { get; private set; }

    public void Initialize(EnemyState StartingState)
    {

    }
    public void ChangeState(EnemyState NewState)
    {
        CurrentState.Exit();
        CurrentState = NewState;
        CurrentState.Enter();
    }



}
