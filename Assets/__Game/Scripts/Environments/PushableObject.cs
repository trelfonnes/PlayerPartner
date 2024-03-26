using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushForce = 1f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        //Switching with the rigidbody types to allow it to be moved, then stopped and then able to be detected by colliders
    } //I'm sure theres a better way to do this.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Partner"))
        {
            Partner partner = collision.gameObject.GetComponent<Partner>();
            if (!partner.stageOne)
            {
                // Get the contact points of the collision
                ContactPoint2D[] contacts = new ContactPoint2D[2];
                int contactCount = collision.GetContacts(contacts);

                if (contactCount > 0)
                {
                    // Calculate the direction of the collision
                    Vector2 collisionDirection = contacts[0].normal;

                    // Calculate the opposite direction to move the object
                    Vector2 pushDirection = -collisionDirection;

                    rb.bodyType = RigidbodyType2D.Dynamic;
                    // Apply the push force to move the object
                    rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                    AudioManager.Instance.PlayAudioClip("Push");
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Partner"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }


}

