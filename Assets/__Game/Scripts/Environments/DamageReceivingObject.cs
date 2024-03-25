using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceivingObject : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth = 2f;
    [SerializeField] float currentHealth;
    [SerializeField] Sprite stationarySprite;
    [SerializeField] List<Sprite> breakingSprites = new List<Sprite>();
    SpriteRenderer sr;

    [SerializeField] float wiggleAmount = .025f;
    [SerializeField] float wiggleDuration = .5f;
    [SerializeField] float wiggleSpeed = 2f;
    Vector3 originalPosition;
    int currentSpriteIndex = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        originalPosition = transform.position;
    }
    public void Damage(float amount, AttackType attackType)
    { 
        StartCoroutine(WiggleCoroutine());
        currentHealth -= amount;
        AudioManager.Instance.PlayAudioClip("Damaged");

        if (currentHealth <= 0)
        {
            // play breaking sprites
            InvokeRepeating("BreakSprites", 0.0f, .1f);
        }
    }

    IEnumerator WiggleCoroutine()
    {
        Vector3 targetLeft = originalPosition + Vector3.left * wiggleAmount;
        Vector3 targetRight = originalPosition + Vector3.right * wiggleAmount;

        float halfDuration = wiggleDuration / 2f;

        float elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float t = elapsedTime / halfDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t); // Apply smoothstep to the interpolation
            transform.position = Vector3.Lerp(originalPosition, targetLeft, smoothT);
            elapsedTime += Time.deltaTime * wiggleSpeed; // Scale the elapsed time by wiggleSpeed
            yield return null;
        }

        // Move the object back to the original position
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float t = elapsedTime / halfDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(targetLeft, originalPosition, smoothT);
            elapsedTime += Time.deltaTime * wiggleSpeed;
            yield return null;
        }

        // Move the object to the right
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float t = elapsedTime / halfDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(originalPosition, targetRight, smoothT);
            elapsedTime += Time.deltaTime * wiggleSpeed;
            yield return null;
        }

        // Move the object back to the original position
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            float t = elapsedTime / halfDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(targetRight, originalPosition, smoothT);
            elapsedTime += Time.deltaTime * wiggleSpeed;
            yield return null;
        }

        // Ensure the object is back to its original position
        transform.position = originalPosition;
    }

    void BreakSprites()
    {
       
        sr.sprite = breakingSprites[currentSpriteIndex];
        currentSpriteIndex++;
        if (currentSpriteIndex >= breakingSprites.Count)
        {
            CancelInvoke("BreakSprites");
            gameObject.SetActive(false);
        }


    }

}
