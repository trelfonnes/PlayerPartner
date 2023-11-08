using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHItBox>
{
    public event Action<Collider2D[]> OnDetectedCollider2D;
    CoreComp<EnemyMovement> movement;
    Vector2 offset;
    Collider2D[] detected;

    void HandleAttackAction()
    {
        //Logic for the combat facing direction of the enemy hit box to be active


        if (detected.Length == 0)
            return;
        OnDetectedCollider2D?.Invoke(detected);
    }

    protected override void Start()
    {
        base.Start();
        movement = new CoreComp<EnemyMovement>(EnemyCore);
        EnemyEventHandler.OnAttackAction += HandleAttackAction;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EnemyEventHandler.OnAttackAction -= HandleAttackAction;
    }

    private void OnDrawGizmosSelected()
    {
        if (dataEnemy == null)
            return;
        foreach(var item in dataEnemy.AttackData)
        {
            //logic for drawing hitboxes while game is running

        }
    }


}
