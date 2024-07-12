using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using TMPro;
public class ObstacleRaceManager : MonoBehaviour
{
    [SerializeField] string nameOfThisObstacle; //This unique key must be filled to prevent best reward from respawning.
    [SerializeField] float raceDuration = 60f;
    [SerializeField] float largeRewardTargetTime;
    [SerializeField] float smallRewardTargetTime;
    [SerializeField] GameObject largeReward; //reference a prefab of a chest containing the desired item.
    [SerializeField] GameObject smallReward; //reference a prefab of a chest containing the desired item.
    [SerializeField] Transform rewardSpawnPoint;
    float remainingTime;
    public TextMeshProUGUI timerText;
    bool raceActive = false;
    bool largeRewardEarned;
    private List<string> passedObstacles = new List<string>();

    //RaceTiming

    void Start()
    {
        remainingTime = raceDuration;
        UpdateTimerUI();
        timerText.gameObject.SetActive(false);
        if (ES3.KeyExists(nameOfThisObstacle))
        {
          largeRewardEarned = ES3.Load<bool>(nameOfThisObstacle);
        }
    }

    void Update()
    {
        if (raceActive)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                EndRace(false); // Time's up and race not completed
            }
        }
    }
    public void StartRace()
    {
        remainingTime = raceDuration;
        timerText.gameObject.SetActive(true);
        raceActive = true;

    }
    private void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(remainingTime).ToString();
    }
    public void EndRace(bool completed)
    {
        raceActive = false;
        timerText.gameObject.SetActive(false);

        if (completed)
        {
            // Determine reward based on remainingTime
            SpawnReward(remainingTime);
        }
        else
        {
            // Handle race failure
            Debug.Log("Race failed. Time's up!");
        }
    }
    private void SpawnReward(float remainingTime)
    {
        if(remainingTime >= largeRewardTargetTime && !largeRewardEarned)
        {
            largeRewardEarned = true;
            Instantiate(largeReward, rewardSpawnPoint.position, rewardSpawnPoint.rotation);
            ES3.Save(nameOfThisObstacle, largeRewardEarned);

        }
        else
        {
            Instantiate(smallReward, rewardSpawnPoint.position, rewardSpawnPoint.rotation);
        }
       
        Debug.Log("Race completed! Remaining time: " + remainingTime);
    }


    //Obstacle Tracking

    public void PassObstacle(string obstacleID)
    {
        if (!passedObstacles.Contains(obstacleID))
        {
            passedObstacles.Add(obstacleID);
            Debug.Log("Obstacle passed: " + obstacleID);
        }
    }

    private bool AllObstaclesPassed()
    {
        // Logic to check if all required obstacles are passed
        // Example: return passedObstacles.Count == totalObstacles;
        return true;
    }
    private void OnEnable()
    {
        Lua.RegisterFunction("StartRace", this, SymbolExtensions.GetMethodInfo(() => StartRace()));
    }
    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(StartRace));
    }
}
