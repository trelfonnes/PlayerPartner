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
    public event Action<string> onPlayAudio;
    public event Action onChangePosition;
    public event Action onStartDefend;
    public event Action onStopDefend;
    public event Action onPursuePlayer;


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
        AudioManager.Instance.PlayAudioClip("Charged");


    }
    void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase); 
    void PlayAudio(string audio)
    {
        AudioManager.Instance.PlayAudioClip(audio);
    }
    void ChangePosition()
    {
        onChangePosition?.Invoke();
    }
    void StartDefending()
    {
        onStartDefend?.Invoke();
    }
    void StopDefending()
    {
        onStopDefend?.Invoke();
    }
    void PursuePlayer()
    {
        onPursuePlayer?.Invoke();
    }
}
