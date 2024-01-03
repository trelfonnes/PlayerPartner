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
        if(movement.Comp.LastEnemyDirection.y > 0 && movement.Comp.LastEnemyDirection.x == 0)
        {//NorthFace
            offset.Set(transform.position.x + (currentAttackDataEnemy.HitBoxNorth.center.x * movement.Comp.LastEnemyDirection.x),
                transform.position.y + currentAttackDataEnemy.HitBoxNorth.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataEnemy.HitBoxNorth.size, 0f, dataEnemy.DetectableLayers);
        }
        else if (movement.Comp.LastEnemyDirection.x > 0 && movement.Comp.LastEnemyDirection.y == 0)
        { //east
            offset.Set(transform.position.x + (currentAttackDataEnemy.HitBoxEast.center.x * movement.Comp.LastEnemyDirection.x),
                transform.position.y + currentAttackDataEnemy.HitBoxEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataEnemy.HitBoxEast.size, 0f, dataEnemy.DetectableLayers);
        }
        //work the flip in movement
        else if(movement.Comp.LastEnemyDirection.y < 0 && movement.Comp.LastEnemyDirection.x == 0)
        { // south
            offset.Set(transform.position.x + (currentAttackDataEnemy.HitBoxSouth.center.x * movement.Comp.LastEnemyDirection.x),
                transform.position.y + currentAttackDataEnemy.HitBoxSouth.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataEnemy.HitBoxSouth.size, 0f, dataEnemy.DetectableLayers);
        }
        else if (movement.Comp.LastEnemyDirection.y > 0 && movement.Comp.LastEnemyDirection.x > 0)
        {
            // draw up right north east
            offset.Set(transform.position.x + (currentAttackDataEnemy.HitBoxNorthEast.center.x * movement.Comp.LastEnemyDirection.x),
            transform.position.y + currentAttackDataEnemy.HitBoxNorthEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataEnemy.HitBoxNorthEast.size, 0f, dataEnemy.DetectableLayers);

        }
        else if (movement.Comp.LastEnemyDirection.y < 0 && movement.Comp.LastEnemyDirection.x > 0)
        {
            //down right south east
            offset.Set(transform.position.x + (currentAttackDataEnemy.HitBoxSouthEast.center.x * movement.Comp.LastEnemyDirection.x),
           transform.position.y + currentAttackDataEnemy.HitBoxSouthEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataEnemy.HitBoxSouthEast.size, 0f, dataEnemy.DetectableLayers);

        }
        else if (movement.Comp.LastEnemyDirection.x < 0 && movement.Comp.LastEnemyDirection.y == 0)
        {
            //West but use Hitbox East because of Flip in movement core

            offset.Set(transform.position.x + (currentAttackDataEnemy.HitBoxEast.center.x * movement.Comp.LastEnemyDirection.x),
            transform.position.y + currentAttackDataEnemy.HitBoxEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataEnemy.HitBoxEast.size, 0f, dataEnemy.DetectableLayers);

        }
        else if (movement.Comp.LastEnemyDirection.y < 0 && movement.Comp.LastEnemyDirection.x < 0)
        {
            //draw diagonl down left south west


            offset.Set(transform.position.x + (currentAttackDataEnemy.HitBoxSouthEast.center.x * movement.Comp.LastEnemyDirection.x),
            transform.position.y + currentAttackDataEnemy.HitBoxSouthEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataEnemy.HitBoxSouthEast.size, 0f, dataEnemy.DetectableLayers);

        }
        else if (movement.Comp.LastEnemyDirection.y > 0 && movement.Comp.LastEnemyDirection.x < 0)
        {
            // up left North west
            Debug.Log("northwest");
            offset.Set(transform.position.x + (currentAttackDataEnemy.HitBoxNorthEast.center.x * movement.Comp.LastEnemyDirection.x),
          transform.position.y + currentAttackDataEnemy.HitBoxNorthEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataEnemy.HitBoxNorthEast.size, 0f, dataEnemy.DetectableLayers);

        }
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
