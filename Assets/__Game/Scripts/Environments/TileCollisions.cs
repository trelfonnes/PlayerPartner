using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollisions : MonoBehaviour
{
    [SerializeField] PhysicsMaterial2D iceMaterial; //less friction
    [SerializeField] PhysicsMaterial2D sandMaterial; // more friction
    [SerializeField] PhysicsMaterial2D normalMaterial;

    private void Start()
    {
        normalMaterial = new PhysicsMaterial2D();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger || collision.CompareTag("Partner") && !collision.isTrigger)
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

            if (gameObject.CompareTag("IceTile")) //TODO ADD more logic for other types of tiles. i.e. Sand
            {
                playerRigidbody.sharedMaterial = iceMaterial;
            }
           else if (gameObject.CompareTag("SandTile")) //TODO ADD more logic for other types of tiles. i.e. Sand
            {
                playerRigidbody.sharedMaterial = sandMaterial;
            }
            else
            {
                playerRigidbody.sharedMaterial = normalMaterial;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            playerRigidbody.sharedMaterial = normalMaterial;
        }
    }
}
