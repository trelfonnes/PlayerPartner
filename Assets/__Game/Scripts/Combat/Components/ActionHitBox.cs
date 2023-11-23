using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHItBox>
{
    public event Action<Collider2D[]> OnDetectedCollider2D;

    CoreComp<Movement> movement;
    Vector2 offset;
    Collider2D[] detected;

    void HandleAttackAction()
    {
        // refactor with a dictionary to cut down on if statements??
        if (movement.Comp.facingCombatDirectionY > 0 && movement.Comp.facingCombatDirectionX == 0)
        {
            //North
            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxNorth.center.x * movement.Comp.facingCombatDirectionX),
            transform.position.y + currentAttackDataPlayer.HitBoxNorth.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxNorth.size, 0f, dataPlayer.DetectableLayers);

        }
        else if (movement.Comp.facingCombatDirectionX > 0 && movement.Comp.facingCombatDirectionY == 0)
        {
            //East
            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxEast.center.x * movement.Comp.facingCombatDirectionX),
            transform.position.y + currentAttackDataPlayer.HitBoxEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxEast.size, 0f, dataPlayer.DetectableLayers);

        }
        else if (movement.Comp.facingCombatDirectionX < 0 && movement.Comp.facingCombatDirectionY == 0)
        {
            //West but use Hitbox East because of Flip in movement core

            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxEast.center.x * movement.Comp.facingCombatDirectionX),
            transform.position.y + currentAttackDataPlayer.HitBoxEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxEast.size, 0f, dataPlayer.DetectableLayers);

        }
        else if (movement.Comp.facingCombatDirectionY < 0 && movement.Comp.facingCombatDirectionX == 0)
        {
            //South
            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxSouth.center.x * movement.Comp.facingCombatDirectionX),
            transform.position.y + currentAttackDataPlayer.HitBoxSouth.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxSouth.size, 0f, dataPlayer.DetectableLayers);

        } 
        else if (movement.Comp.facingCombatDirectionY == 0 && movement.Comp.facingCombatDirectionX == 0)
        {
            //South
            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxSouth.center.x * movement.Comp.facingCombatDirectionX),
            transform.position.y + currentAttackDataPlayer.HitBoxSouth.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxSouth.size, 0f, dataPlayer.DetectableLayers);

        }

        else if (movement.Comp.facingCombatDirectionY < 0 && movement.Comp.facingCombatDirectionX < 0)
        {
            //draw diagonl down left south west


            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxSouthEast.center.x * movement.Comp.facingCombatDirectionX),
            transform.position.y + currentAttackDataPlayer.HitBoxSouthEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxSouthEast.size, 0f, dataPlayer.DetectableLayers);

        }
        else if (movement.Comp.facingCombatDirectionY > 0 && movement.Comp.facingCombatDirectionX > 0)
        {
            // draw up right north east
            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxNorthEast.center.x * movement.Comp.facingCombatDirectionX),
            transform.position.y + currentAttackDataPlayer.HitBoxNorthEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxNorthEast.size, 0f, dataPlayer.DetectableLayers);

        }
        else if (movement.Comp.facingCombatDirectionY < 0 && movement.Comp.facingCombatDirectionX > 0)
        {
            //down right south east
            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxSouthEast.center.x * movement.Comp.facingCombatDirectionX),
           transform.position.y + currentAttackDataPlayer.HitBoxSouthEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxSouthEast.size, 0f, dataPlayer.DetectableLayers);

        }
        else if (movement.Comp.facingCombatDirectionY > 0 && movement.Comp.facingCombatDirectionX < 0)
        {
            // up left North west
            Debug.Log("northwest");
            offset.Set(transform.position.x + (currentAttackDataPlayer.HitBoxNorthEast.center.x * movement.Comp.facingCombatDirectionX),
          transform.position.y + currentAttackDataPlayer.HitBoxNorthEast.center.y);
            detected = Physics2D.OverlapBoxAll(offset, currentAttackDataPlayer.HitBoxNorthEast.size, 0f, dataPlayer.DetectableLayers);

        }



        if (detected.Length == 0)
            return;

        OnDetectedCollider2D?.Invoke(detected);

}

    protected override void Start()
    {
        base.Start();
        movement = new CoreComp<Movement>(PlayerCore);
        PlayerEventHandler.OnAttackAction += HandleAttackAction;

    }
   
    protected override void OnDestroy()
    {
        base.OnDestroy();
        PlayerEventHandler.OnAttackAction -= HandleAttackAction;
    }
    private void OnDrawGizmosSelected()
    {
        if (dataPlayer == null)
            return;

        foreach (var item in dataPlayer.AttackData)
        {
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
