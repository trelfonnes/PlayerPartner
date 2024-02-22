using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : BossCoreComponent
{
   [SerializeField] BossWeapon weapon;
        BossMeleeState currentState;
    bool executeAttack;

    private void Start()
    {
        weapon = GetComponentInChildren<BossWeapon>();
        weapon.onExit += AnimationFinished;
        weapon.SetComponentLocator(componentLocator);
    }
    private void Update()
    {
        if (executeAttack)
        {
            currentState = BossMeleeState.active;
        }
        
        
    }
    public void ExecuteAttack() // called by node
    {
        executeAttack = true;
        weapon.Enter();
    }
    public void AnimationFinished() //called by anim event
    {
        executeAttack = false;
        currentState = BossMeleeState.idle;
    }

    public BossMeleeState GetCurrentMeleeState()
    {
        return currentState;
    }
    private void OnDisable()
    {
        weapon.onExit -= AnimationFinished;

    }
}
public enum BossMeleeState
{
    idle,
    active,
    coolDown
}

