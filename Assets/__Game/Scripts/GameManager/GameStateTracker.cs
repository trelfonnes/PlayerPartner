using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    overworld,
    Dungeon,
    Arena,
    gameStart,
    gameOver

}

public class GameStateTracker : MonoBehaviour
{
    public static GameStateTracker Instance;
    public GameState CurrentGameState { get; private set; }
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        CurrentGameState = GameState.gameStart;
        
    }
    public void ChangeGameState(GameState newState)
    {
        CurrentGameState = newState;
    }
    public GameState ReturnGameState()
    {
        return CurrentGameState;
    }
   
}