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

    void AnimationFinishedTrigger() => OnFinish?.Invoke();
    void StartMovementTrigger() => OnStartMovement?.Invoke();
    void StopMovementTrigger() => OnStopMovement?.Invoke();
    void AttackActionTrigger() => OnAttackAction.Invoke();
}
