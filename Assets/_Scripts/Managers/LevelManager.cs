/// <summary>
/// Responsible for the level rules, keeps track of the platforms in the level and trigger new game states 
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlatformBehaviour;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _platfomsCount;
    [SerializeField] private Transform _platformsParent;
    [SerializeField] private int _avaiableMoves;

    // Subscribe and Unsbscribe to Event
    private void OnEnable()
    {
        PlatformBehaviour.PlatformDestroyed += PlatformDestroyed;
    }
    private void OnDisable()
    {
        PlatformBehaviour.PlatformDestroyed -= PlatformDestroyed;
    }

    private void Start()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.ChoosingAvatar);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void StartLevel()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.InGame);
        // DetectAvaiableMoves();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void PlatformDestroyed()
    {
        if (GameManager.Instance.State == GameManager.GameState.InGame)
        {
            DetectAvaiableMoves();
        }
    }

    private void DetectAvaiableMoves()
    {
        _avaiableMoves = 0;

        for (int i = 0; i < _platformsParent.childCount; i++)
        {
            try
            {
                if (_platformsParent.GetChild(i).GetComponent<PlatformBehaviour>().CurrentState == PlatformState.selectable)
                {
                    _avaiableMoves++;
                }
            }
            catch
            {
                // No platform detected
            }
        }
        _platfomsCount = _platformsParent.childCount;

        if (_platfomsCount == 2)
        {
            GameManager.Instance.AudioManagerReference?.PlaySFX(3);
            GameManager.Instance.ChangeGameState(GameManager.GameState.Victory);
            return;
        }

        if (_avaiableMoves == 0)
        {
            GameManager.Instance.AudioManagerReference?.PlaySFX(2);
            GameManager.Instance.ChangeGameState(GameManager.GameState.Trapped);
        }
    }
}
