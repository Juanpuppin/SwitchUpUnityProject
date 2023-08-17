/// <summary>
/// Responsible for toggling the visibility of the canvas depending of the current game state
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _chooseAvatarCanvas;
    [SerializeField] private GameObject _victoryCanvas;
    [SerializeField] private GameObject _trappedCanvas;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += GameStateChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from OnGameStateChange
        GameManager.Instance.OnGameStateChange -= GameStateChanged;
    }

    private void GameStateChanged(GameManager.GameState state)
    {
        // Refactor

        if (state == GameManager.GameState.ChoosingAvatar)
        {
            _ui.SetActive(false);
            _chooseAvatarCanvas.SetActive(true);
            _victoryCanvas.SetActive(false);
            _trappedCanvas.SetActive(false);

        }

        if (state == GameManager.GameState.InGame)
        {
            _ui.SetActive(true);
            _chooseAvatarCanvas.SetActive(false);
            _victoryCanvas.SetActive(false);
            _trappedCanvas.SetActive(false);
        }

        if (state == GameManager.GameState.Victory)
        {
            _ui.SetActive(false);
            _chooseAvatarCanvas.SetActive(false);
            _victoryCanvas.SetActive(true);
            _trappedCanvas.SetActive(false);
        }

        if (state == GameManager.GameState.Trapped)
        {
            _ui.SetActive(false);
            _chooseAvatarCanvas.SetActive(false);
            _victoryCanvas.SetActive(false);
            _trappedCanvas.SetActive(true);
        }
    }
}



