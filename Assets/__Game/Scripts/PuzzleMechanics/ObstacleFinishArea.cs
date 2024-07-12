using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFinishArea : MonoBehaviour
{
    bool playerFinished = false;
    bool partnerFinished = false;
    [SerializeField] ObstacleRaceManager thisObstacleManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerFinished = true;
            CheckRaceCompletion();
        }
        else if (collision.CompareTag("Partner"))
        {
            partnerFinished = true;
            CheckRaceCompletion();
        }
    }

    void CheckRaceCompletion()
    {
        if(playerFinished && partnerFinished)
        {
            thisObstacleManager.EndRace(true);
        }
    }


}
