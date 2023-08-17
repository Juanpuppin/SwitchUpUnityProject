/// <summary>
/// Controls the overall flow of the game states
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager AudioManagerReference;
    public GameState State = new GameState();

    public event Action<GameState> OnGameStateChange;

    private void Awake()
    {
        transform.parent = null;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void OnDisable()
    {
        OnGameStateChange -= GameManager_OnGameStateChange;
    }

    private void GameManager_OnGameStateChange(GameState state)
    {
        print("Game state " + state);
    }


    public void ChangeGameState(GameState newState)
    {
        State = newState;
        OnGameStateChange(newState);
    }

    public enum GameState
    {
        Loading,
        ChoosingAvatar,
        InGame,
        Trapped,
        Victory,
    }
}