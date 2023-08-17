/// <summary>
/// Used to play the button click sound effect
/// </summary    

using UnityEngine;
using UnityEngine.UI;


public class PlayButtonSfx : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Cliked);
    }


    private void Cliked() 
    {
        GameManager.Instance.AudioManagerReference?.PlaySFX(0);
    }
}
