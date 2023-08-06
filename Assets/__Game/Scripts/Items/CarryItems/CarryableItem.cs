using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryableItem : MonoBehaviour, ICarry, IThrow
{
    private Rigidbody2D rb;
    private GameObject item;
    private Transform itemTransform;
    [SerializeField] float throwSpeed;
    [SerializeField] float setDownDistance;
    private BoxCollider2D boxCollider;
    private Vector2 initialPosition;
    private Vector2 modifiedDirection;
    private bool isThrown = false;
    private bool wasKinematic;
    [SerializeField]private float throwDownwardForceMultiplier;
    [SerializeField]private float setDownwardForceMultiplier;
    [SerializeField] float setUpwardForceMultiplier;

    private void Start()
    {
        itemTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        initialPosition = transform.position;
        wasKinematic = rb.isKinematic;
    }
    private void Update()
    {
        if(isThrown && rb.velocity.magnitude < .1f)
        {
            Land();
        }
    }

    private void Land()
    {

        isThrown = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = wasKinematic;
        Physics2D.IgnoreLayerCollision(7, 10, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);

        // transform.position = initialPosition;
    }

    public void Carry(Transform CarryPoint)
    {
        
            //rb.simulated = true;
            rb.isKinematic = true;
            itemTransform.position = CarryPoint.position;
            itemTransform.parent = CarryPoint;
            Physics2D.IgnoreLayerCollision(7, 10, true);
        
    }

    public void Throw(Vector2 direction)
    {
        itemTransform.parent = null;

        if (direction == new Vector2(5,0) || direction == new Vector2(-5,0))
        {
            modifiedDirection = direction + Vector2.down * throwDownwardForceMultiplier;
            Debug.Log(modifiedDirection);
        }
        else
       {
         modifiedDirection = direction;
       }
        if (!isThrown && rb != null)
        {
            rb.velocity = Vector2.zero;
            Physics2D.IgnoreLayerCollision(7, 10, true);
           
            rb.isKinematic = false;
            rb.AddForce(modifiedDirection * throwSpeed, ForceMode2D.Impulse);
            isThrown = true;

        }
    }

    public void SetDown(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            modifiedDirection = direction * setUpwardForceMultiplier;
        }
        else
        {
            modifiedDirection = direction + Vector2.down * setDownwardForceMultiplier;

        }
        if (!isThrown && rb != null)
        {
            rb.velocity = Vector2.zero;
            Physics2D.IgnoreLayerCollision(7, 9, true);
            Physics2D.IgnoreLayerCollision(7, 10, true);
            itemTransform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(modifiedDirection * setDownDistance, ForceMode2D.Impulse);
            isThrown = true;
         

        }

    }
  

}
