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
    public event Action<AttackPhases> OnEnterAttackPhase;

    void AnimationFinishedTrigger() => OnFinish?.Invoke();
    void StartMovementTrigger() => OnStartMovement?.Invoke();
    void StopMovementTrigger() => OnStopMovement?.Invoke();
    void AttackActionTrigger() => OnAttackAction?.Invoke();
    void MinHoldPassTrigger() => OnMinHoldPassed?.Invoke();
    void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase); 

}
