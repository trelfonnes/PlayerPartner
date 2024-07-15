using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryableItem : MonoBehaviour, ICarry, IThrow
{
    private Rigidbody2D rb;
    private GameObject item;
    private Transform itemTransform;
    public bool isHeavyCarryable;
    [SerializeField] float throwSpeed;
    [SerializeField] float setDownDistance;
    private BoxCollider2D boxCollider;
    private Vector2 initialPosition;
    private Vector2 modifiedDirection;
    private bool isThrown = false;
    private bool wasKinematic;
    [SerializeField]private float throwInitialDownwardForceMultiplier;
    private float throwDownwardForceMultiplier;
    private float throwUpwardForceMultiplier;

    [SerializeField]private float throwInitialUpwardForceMultiplier;
    [SerializeField]private float setDownwardForceMultiplier;
    [SerializeField] float setUpwardForceMultiplier;
    [SerializeField] float airTravelTime;
    SpriteRenderer sr;
    [SerializeField] private float throwDistanceLimit = 5f;
    private Vector2 throwStartPosition;
    [SerializeField] private float smoothTransitionTime;
    bool landAppropriate;

    private void Start()
    {
        itemTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        initialPosition = transform.position;
        wasKinematic = rb.isKinematic;
        sr = GetComponent<SpriteRenderer>();
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
        Debug.Log("land is called");
        isThrown = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = wasKinematic;
        if (landAppropriate)
        {
            Physics2D.IgnoreLayerCollision(7, 10, false);
            Physics2D.IgnoreLayerCollision(7, 9, false);
            Physics2D.IgnoreLayerCollision(7, 18, false);
            Physics2D.IgnoreLayerCollision(7, 19, false);
            sr.sortingOrder = 0;
        }
        // transform.position = initialPosition;
    }

    public void Carry(Transform CarryPoint, bool canCarryHeavy)
    {
        if (!canCarryHeavy && isHeavyCarryable)
        {
            Debug.Log("ThisItemIsTooHeavyFor you!");
            return;
        }
        else
        {
            //rb.simulated = true;
            rb.isKinematic = true;
            itemTransform.position = CarryPoint.position;
            itemTransform.parent = CarryPoint;
            Physics2D.IgnoreLayerCollision(7, 10, true);
            Physics2D.IgnoreLayerCollision(7, 19, true);
            sr.sortingOrder = 1;
        }
    }

    public void Throw(Vector2 direction)
    {
        landAppropriate = false;
        itemTransform.parent = null;
     
        if (direction.x > .05f || direction.x < -.05f)
        {
            if (direction.y > 0)
            {
                throwUpwardForceMultiplier = throwInitialUpwardForceMultiplier * .75f;
            }
            else if (direction.y < 0)
            {
                throwUpwardForceMultiplier = throwInitialUpwardForceMultiplier * .5f;
            }
            else
            {
                throwUpwardForceMultiplier = throwInitialUpwardForceMultiplier;
            }
            modifiedDirection = direction + Vector2.up * throwUpwardForceMultiplier;
            StartCoroutine(ApplyDownwardForceAfterDelay(direction));
        }
        else
        {
            modifiedDirection = direction;
        }

        if (!isThrown && rb != null)
        {
            rb.velocity = Vector2.zero;
            Physics2D.IgnoreLayerCollision(7, 9, true);
            Physics2D.IgnoreLayerCollision(7, 10, true);
            Physics2D.IgnoreLayerCollision(7, 18, true);
            Physics2D.IgnoreLayerCollision(7, 19, true);

            rb.isKinematic = false;
            StartCoroutine(SmoothTransitionToVelocity(modifiedDirection.normalized * throwSpeed));
            throwStartPosition = itemTransform.position;
            StartCoroutine(TrackThrowDistance());

            isThrown = true;
        }
    }

    private IEnumerator ApplyDownwardForceAfterDelay(Vector2 direction)
    {
        yield return new WaitForSeconds(airTravelTime); // Adjust the delay duration as needed
        if(direction.y > 0)
        {
            throwDownwardForceMultiplier = throwInitialDownwardForceMultiplier * .5f;
        }
        else if(direction.y < 0)
        {
            throwDownwardForceMultiplier = throwInitialDownwardForceMultiplier * 2f;
        }
        else
        {
            throwDownwardForceMultiplier = throwInitialDownwardForceMultiplier;
        }
        Debug.Log(throwDownwardForceMultiplier + "ThrowDownwardForceMultiplier");
        modifiedDirection = direction + Vector2.down * throwDownwardForceMultiplier;
        Debug.Log(modifiedDirection);
        // Set downward velocity smoothly
        StartCoroutine(SmoothTransitionToVelocity(modifiedDirection.normalized * throwSpeed));
        isThrown = true;
    }

    private IEnumerator SmoothTransitionToVelocity(Vector2 targetVelocity)
    {
        float elapsedTime = 0f;
        Vector2 initialVelocity = rb.velocity;

        while (elapsedTime < smoothTransitionTime)
        {
            rb.velocity = Vector2.Lerp(initialVelocity, targetVelocity, elapsedTime / smoothTransitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = targetVelocity;
    }
    private IEnumerator TrackThrowDistance()
    {
        float initialDistance = Vector2.Distance(itemTransform.position, throwStartPosition);

        while (initialDistance + throwDistanceLimit > Vector2.Distance(itemTransform.position, throwStartPosition))
        {
            yield return null;
        }

        // Stop the object after reaching the distance limit
        rb.velocity = Vector2.zero;
        landAppropriate = true;
        Land();
    }
    public void SetDown(Vector2 direction)
    {
        landAppropriate = true;
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
            Physics2D.IgnoreLayerCollision(7, 18, true);
            Physics2D.IgnoreLayerCollision(7, 19, true);

            itemTransform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(modifiedDirection * setDownDistance, ForceMode2D.Impulse);
            isThrown = true;
         

        }

    }

  
}
