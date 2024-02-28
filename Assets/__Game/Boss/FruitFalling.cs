using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitFalling : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 1f;
    [SerializeField] private float amountToIncrease = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger || collision.CompareTag("Partner") && !collision.isTrigger || collision.CompareTag("boss") && !collision.isTrigger)
        {
            collision.GetComponentInChildren<IHealthChange>().IncreaseHealth(amountToIncrease);
            gameObject.SetActive(false);
        }
    }
    // Method to make the fruit fall
    public void Fall()
    {
        // Start the coroutine to make the fruit fall
        StartCoroutine(FallCoroutine());
    }

    // Coroutine to make the fruit fall gradually
    private IEnumerator FallCoroutine()
    {
        // Get the initial position of the fruit
        Vector3 initialPosition = transform.position;

        // Calculate the target position (1 unit down on the Y-axis)
        Vector3 targetPosition = initialPosition + Vector3.down;

        // Loop until the fruit reaches the target position
        while (transform.position.y > targetPosition.y)
        {
            // Move the fruit downwards gradually
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallSpeed * Time.deltaTime);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the fruit is exactly at the target position
        transform.position = targetPosition;
    }
}
