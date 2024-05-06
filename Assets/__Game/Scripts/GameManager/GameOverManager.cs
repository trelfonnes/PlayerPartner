using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] StatEvents statEvents;

    bool isArenaBattle;
    SceneLoaderUtility sceneLoader = new SceneLoaderUtility();

    private void Start()
    {
        if(GameStateTracker.Instance.CurrentGameState == GameState.Arena) //the only instance of game over when partner dies
        {
            isArenaBattle = true;
        }
        else
        {
            isArenaBattle = false;
        }
        Debug.Log(GameStateTracker.Instance.CurrentGameState + "Message from the GameOverManager Start()");
        if (isArenaBattle)
        {
            statEvents.onCurrentHealthZero += TriggerGameOver;
        }
        else
        {
            statEvents.onPlayerHealthZero += TriggerGameOver;
        }
    }

    void TriggerGameOver()
    {
        sceneLoader.LoadScene("GameOver");
    }

    private void OnDisable()
    {
        statEvents.onCurrentHealthZero -= TriggerGameOver;
        statEvents.onPlayerHealthZero -= TriggerGameOver;
    }
}
