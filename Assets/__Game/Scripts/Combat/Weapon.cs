using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{

    Animator anim;
    GameObject baseGO;
    Player player;
  public void Enter()
    {
        print($"{transform.name} enter");
        anim.SetBool("active", true);
        anim.SetFloat("moveX", player.lastDirection.x);
        anim.SetFloat("moveY", player.lastDirection.y);

    }

    private void Awake()
    {
        baseGO = transform.Find("Base").gameObject;
        player = GetComponentInParent<Player>();
        anim = baseGO.GetComponent<Animator>();

        
    }
}
