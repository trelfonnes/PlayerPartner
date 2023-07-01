using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PartnerWeapon : MonoBehaviour
{
    public event Action onExit;
    Animator anim;
    GameObject baseGO;
    Partner partner;
    AnimationEventHandler eventHandler;
    public void Enter()
    {
        print($"{transform.name} enter");
        anim.SetBool("active", true);
        anim.SetFloat("moveX", partner.lastDirection.x);
        anim.SetFloat("moveY", partner.lastDirection.y);
    }
    void Exit()
    {
        anim.SetBool("active", false);
        onExit?.Invoke();
    }
    private void Awake()
    {
        baseGO = transform.Find("Base").gameObject;
        partner = GetComponentInParent<Partner>();

        anim = baseGO.GetComponent<Animator>();
        eventHandler = baseGO.GetComponent<AnimationEventHandler>();
        
    }

    private void OnEnable()
    {
        eventHandler.OnFinish += Exit;
    }
    private void OnDisable()
    {
        eventHandler.OnFinish -= Exit;
    }
}
