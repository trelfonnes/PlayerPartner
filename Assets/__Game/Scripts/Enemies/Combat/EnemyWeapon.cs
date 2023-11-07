using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject BaseGO { get; private set; }
    public GameObject WeaponSpriteGO { get; private set; }
   Enemy enemy;
    public AnimationEventHandler EventHandler { get; private set; }
    public CoreHandler Core { get; private set; }
    public event Action onExit;
    public event Action onEnter;
    public event Action<bool> OncurrentInputChange;
    Animator anim;

    private void Awake()
    {
        BaseGO = transform.Find("Base").gameObject;
        WeaponSpriteGO = transform.Find("WeaponSprite").gameObject; // If this is too non performant I can serialize and drag in game objects.
        enemy = GetComponentInParent<Enemy>();
        anim = BaseGO.GetComponent<Animator>();
        EventHandler = BaseGO.GetComponent<AnimationEventHandler>();
    }
    public void Enter()
    {
        anim.SetBool("active", true);
        anim.SetFloat("moveX", enemy.enemyDirection.x); //might need to store "latest direction" like I did with player
        anim.SetFloat("moveY", enemy.enemyDirection.y);
        onEnter?.Invoke();
    }

    void Exit()
    {
        anim.SetBool("active", false);
        onExit.Invoke();
    }
    private void OnEnable()
    {
        EventHandler.OnFinish += Exit;
    }

    private void OnDisable()
    {
        EventHandler.OnFinish -= Exit;
    }
}
