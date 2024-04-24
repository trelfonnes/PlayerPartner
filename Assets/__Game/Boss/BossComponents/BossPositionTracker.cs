using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPositionTracker : BossCoreComponent
{
    public Transform playerTransform;
    public float predictionTime = 0.5f; // Time to predict ahead in seconds
  
    Vector2 PredictedPosition;
    private Queue<Vector2> playerPositionHistory = new Queue<Vector2>();

    private Vector2 smoothedAverageDirection = Vector2.zero;



    void AddPlayerPositionToHistory()
    {
        // Add current player position to history
        playerPositionHistory.Enqueue(playerTransform.position);

        // Remove oldest position if history size exceeds desired length
        if (playerPositionHistory.Count > 10)
        {
            playerPositionHistory.Dequeue();
        }
    }

    Vector2 PredictPlayerFuturePosition()
    {
        // Smooth out player movement data
        smoothedAverageDirection = SmoothPlayerMovement();

        // Predict future position based on smoothed direction and prediction time
        return (Vector2)playerTransform.position + smoothedAverageDirection * predictionTime;
    }
    public Vector2 GetPredictedPosition()
    {
        return PredictedPosition;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        AddPlayerPositionToHistory();

        // Predict player's future position
        Vector2 predictedPosition = PredictPlayerFuturePosition();

        // Aim projectile at predicted position
       PredictedPosition = (predictedPosition - (Vector2)transform.position).normalized;

       
    }

    Vector2 SmoothPlayerMovement()
    {
        // Calculate average direction of player movement using a simple moving average
        Vector2 sumDirection = Vector2.zero;
        foreach (Vector2 position in playerPositionHistory)
        {
            sumDirection += position - playerPositionHistory.Peek();
        }
        return sumDirection / playerPositionHistory.Count;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner"))
        {
            playerTransform = collision.transform;
        }
    }
}
