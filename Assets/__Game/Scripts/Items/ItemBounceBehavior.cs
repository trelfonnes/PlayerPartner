using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBounceBehavior : MonoBehaviour
{
    [SerializeField] float bounceIntensity;

    private void Start()
    {
        Vector2 randomDirection = GetRandomDirection();
        float bounceDuration = .25f;
        StartCoroutine(BounceCoroutine(transform, randomDirection, bounceDuration, bounceIntensity));
    }


    private Vector2 GetRandomDirection()
    {
        Vector2[] directions = new Vector2[]
        {
              new Vector2(1, 0), // Right
        new Vector2(-1, 0), // Left
        new Vector2(0, 1), // Up
        new Vector2(0, -1), // Down
        new Vector2(1, 1).normalized, // Up-Right
        new Vector2(-1, 1).normalized, // Up-Left
        new Vector2(1, -1).normalized, // Down-Right
        new Vector2(-1, -1).normalized // Down-Left
        };
        return directions[Random.Range(0, directions.Length)];

    }
    private IEnumerator BounceCoroutine(Transform itemTransform, Vector2 direction, float duration, float bounceIntensity)
    {
        float elapsedTime = 0f;
        Vector2 originalPosition = itemTransform.position;
        float bounceHeight = .5f * bounceIntensity;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float height = Mathf.Lerp(bounceHeight, 0, t);
            float yOffset = Mathf.Sin(t * Mathf.PI) * height;

            // Calculate new position
            Vector2 newPosition = originalPosition + direction * t * bounceIntensity;
            newPosition.y += yOffset;

            // Apply the position to the item
            itemTransform.position = newPosition;

            yield return null;
        }
        itemTransform.position = originalPosition + direction * bounceIntensity;

    }

}
