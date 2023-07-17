using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerInputHold : WeaponComponent //using base class so not to pass in attack data
{
    Animator anim;
    bool input;
    bool minHoldPassed;

    void SetAnimatorParameter()//the commented code is for minimum hold times via animation event
                               //in "anticipation" My animations are single frame so it doesn't work with mine
                               // if I have a long anticipation animation, uncomment to produce minimum hold 
                               //time via the anim event in anticipation anims
    {
        // if (input)
        // {
        anim.SetBool("hold", input);
           // return;
       // }
        //if (minHoldPassed)
       // {
        //    anim.SetBool("hold", input);
       // }
    }
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        partnerWeapon.OncurrentInputChange += HandleCurrentInputChange;
        PartnerEventHandler.OnMinHoldPassed += HandleMinHoldPassed;

    }
    protected override void HandleEnter()
    {
        base.HandleEnter();
        minHoldPassed = false;
    }

    void HandleCurrentInputChange(bool newInput)
    {
        input = newInput;
        SetAnimatorParameter();
    }
    
    void HandleMinHoldPassed()
    {
        minHoldPassed = true;
        SetAnimatorParameter();
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        partnerWeapon.OncurrentInputChange -= HandleCurrentInputChange;
        PartnerEventHandler.OnMinHoldPassed -= HandleMinHoldPassed;

    }

}
