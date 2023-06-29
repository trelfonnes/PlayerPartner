using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerWeapon : MonoBehaviour
{
    Animator anim;
    GameObject baseGO;
    Partner partner;
    public void Enter()
    {
        print($"{transform.name} enter");
        anim.SetBool("active", true);
        anim.SetFloat("moveX", partner.lastDirection.x);
        anim.SetFloat("moveY", partner.lastDirection.y);
    }

    private void Awake()
    {
        baseGO = transform.Find("Base").gameObject;
        partner = GetComponentInParent<Partner>();

        anim = baseGO.GetComponent<Animator>();

        
    }
}
