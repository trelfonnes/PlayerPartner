using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackReceiver : CoreComponent, IKnockBackable
{
    [SerializeField] float maxKnockBackTime = .2f;
    float KnockBackStartTime;
    bool isKnockBackActive;

    private CoreComp<Movement> movement;
    private CoreComp<CollisionSenses> collisionSenses;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckKnockBack();
    }

    public void KnockBack(Vector2 angle, float strength, int directionX, int directionY)
    {
        Debug.Log("KNockback applied on " + gameObject.name);
        movement.Comp?.SetKnockBackVelocity(angle, strength, directionX, directionY);
        movement.Comp.CanSetVelocity = false;
        isKnockBackActive = true;

    }

    void CheckKnockBack()
    {
        if (isKnockBackActive || Time.time >= KnockBackStartTime + maxKnockBackTime) // extra condition for a side scroller to include grounded and no y velocity being applied
        {
            isKnockBackActive = false;
            movement.Comp.CanSetVelocity = true;
        }
        

        
    }

    protected override void Awake()
    {
        base.Awake();
        
        movement = new CoreComp<Movement>(core);
        collisionSenses = new CoreComp<CollisionSenses>(core);
    }

}
