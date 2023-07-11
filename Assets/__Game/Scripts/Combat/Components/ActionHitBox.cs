using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHItBox>
{
    private event Action<Collider2D[]> OnDetectedCollider2D;

    CoreComp<Movement> movement;
    Vector2 offset;
    Collider2D[] detected;

    void HandleAttackAction()
    {
      //  offset.Set(
       //             transform.position.x + (currentAttackDataPlayer.HitBox.center.x * movement.Comp.facingCombatDirectionX),
        //            transform.position.y + (currentAttackDataPlayer.HitBox.center.y * movement.Comp.facingCombatDirectionY)); // the function where the combat direction is mulitplied into the hitbox position


     //   detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBox.size, 0f, dataPartner.DetectableLayers);

        if (detected.Length == 0)
            return;

        OnDetectedCollider2D?.Invoke(detected);

        //loop to make sure things are working without next step components
        foreach (var item in detected)
        {
            Debug.Log(item.name);
        }
    }

    protected override void Start()
    {
        base.Start();
        movement = new CoreComp<Movement>(PlayerCore);
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerEventHandler.OnAttackAction += HandleAttackAction;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerEventHandler.OnAttackAction -= HandleAttackAction;
    }
    private void OnDrawGizmosSelected()
    {
        if (dataPlayer == null)
            return;

        foreach (var item in dataPlayer.AttackData)
        {
          //  if (!item.Debug)
               // continue;

          //  Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
        }
    }
}
