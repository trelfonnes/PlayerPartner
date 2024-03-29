using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnStartMovement;
    public event Action OnStopMovement;
    public event Action OnAttackAction;
    public event Action OnMinHoldPassed;
    public event Action OnShootProjectile;
    public event Action OnShootChargedProjectile;
    public event Action<AttackPhases> OnEnterAttackPhase;

    bool isCharged = false;

    void AnimationFinishedTrigger() => OnFinish?.Invoke();
    void StartMovementTrigger() => OnStartMovement?.Invoke();
    void StopMovementTrigger() => OnStopMovement?.Invoke();
    void AttackActionTrigger()
    {
        Debug.Log("AttackAnimTrigger");
        OnAttackAction?.Invoke();
    }
    void MinHoldPassTrigger() => OnMinHoldPassed?.Invoke();
    void ShootProjectileTrigger()
    {
        if (isCharged) //when shot anim event is triggered, it checks if SetChargedState was called
                        //if so, it invokes shootChargedProjectile instead of onShootProjectile
        {
            OnShootChargedProjectile?.Invoke();
            isCharged = false;
        }
        else
        {
            OnShootProjectile?.Invoke();//can create event to have a dropdown like Phases that designates which projectile to shoot??
        }

    }
        void SetChargedState()// this event is triggered in idle after a duration
    {
        isCharged = true;

    }
    void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase); 

}
