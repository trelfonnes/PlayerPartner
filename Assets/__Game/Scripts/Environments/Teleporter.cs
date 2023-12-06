using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] StoredParticles particles;

    public Transform playerStoredTransform;
    public Transform partnerStoredTransform;


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.CompareTag("Player"))
        {
            playerStoredTransform = collision.transform;
        }
        if (!collision.isTrigger && collision.CompareTag("Partner"))
        {
            partnerStoredTransform = collision.transform;
        }

        // Check if both player and partner are present
        if (partnerStoredTransform != null && playerStoredTransform != null)
        {
            Teleport(playerStoredTransform, partnerStoredTransform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStoredTransform = null;
        }
        if (collision.CompareTag("Partner"))
        {
            partnerStoredTransform = null;
        }
    }
    void Teleport(Transform playerTransform, Transform partnerTransform)
    {
        StartCoroutine(TeleportSequence(playerTransform, partnerTransform));
    }

    private IEnumerator TeleportSequence(Transform playerTransform, Transform partnerTransform)
    {
        // Instantiate and play the flash effect
        GameObject flashEffect = Instantiate(particles.GetParticlePrefab(ParticleType.Teleport), transform.position + new Vector3(0f, 0.3f, 0f), Quaternion.Euler(0f, 0f, -90f)); yield return new WaitForSeconds(1.0f); // Adjust the duration as needed

        // Teleport both player and partner
        if (destination)
        {
           // Teleporter destinationTeleporter = teleporterPairs[pairingID];
            
                playerTransform.position = destination.position;
                partnerTransform.position = destination.position;
            
        }

        else
        {
            Debug.LogWarning("Teleporter with " + destination + " not found!");
        }
        Destroy(flashEffect);

    }
}
