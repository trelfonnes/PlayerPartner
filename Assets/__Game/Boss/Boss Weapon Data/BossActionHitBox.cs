using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHItBox>
{
    public event Action<Collider2D[]> OnDetectedCollider2D;
    Vector2 offset;
    Collider2D[] detected;

    void HandleAttackAction()
    {
        
        //work the flip in movement
        // south
            offset.Set(transform.position.x + (currentAttackDataBoss.HitBoxSouth.center.x),
                transform.position.y + currentAttackDataBoss.HitBoxSouth.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataBoss.HitBoxSouth.size, 0f, dataBoss.DetectableLayers);
        
       
        if (detected.Length == 0)
            return;
        OnDetectedCollider2D?.Invoke(detected);
    }

    protected override void Start()
    {
        base.Start();
        BossEventHandler.OnAttackAction += HandleAttackAction;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        BossEventHandler.OnAttackAction -= HandleAttackAction;
    }

    private void OnDrawGizmosSelected()
    {
        if (dataBoss == null)
            return;
        foreach (var item in dataBoss.AttackData)
        {
            //logic for drawing hitboxes while game is running
            if (item.DebugHitBoxNorth)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBoxNorth.center, item.HitBoxNorth.size);

            }
            if (item.DebugHitBoxSouth)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBoxSouth.center, item.HitBoxSouth.size);

            }
            if (item.DebugHitBoxEast)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBoxEast.center, item.HitBoxEast.size);

            }
            if (item.DebugHitBoxWest)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBoxWest.center, item.HitBoxWest.size);

            }
            if (item.DebugHitBoxNorthWest)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBoxNorthWest.center, item.HitBoxNorthWest.size);

            }
            if (item.DebugHitBoxSouthWest)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBoxSouthWest.center, item.HitBoxSouthWest.size);

            }
            if (item.DebugHitBoxNorthEast)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBoxNorthEast.center, item.HitBoxNorthEast.size);

            }
            if (item.DebugHitBoxSouthEast)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBoxSouthEast.center, item.HitBoxSouthEast.size);

            }
        }
    }

}
