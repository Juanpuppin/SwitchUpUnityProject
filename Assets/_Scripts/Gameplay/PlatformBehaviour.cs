/// <summary>
/// Responsible for the behaviour of each instance of platform
/// </summary>

using System;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public PlatformState CurrentState;

    public static int avaiableMoves;
    public static int platformsCount;
    public static event Action<Vector3> PlatformClicked;
    public static event Action PlatformDestroyed;

    [SerializeField] private bool _isTheStartPlatform;
    private Animator _animator;


    // Starting Methods 
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        platformsCount++;

        if (_isTheStartPlatform)
        {
            SelectThisPlatform();

        }
    }

    // Subscribe and Unsbscribe to Event
    private void OnEnable()
    {
        PlatformClicked += PlatformSelectedEvent;
    }
    private void OnDisable()
    {
        PlatformClicked -= PlatformSelectedEvent;
    }

    // Inputs
    private void OnMouseUpAsButton()
    {
        if (GameManager.Instance.State != GameManager.GameState.InGame)
        {
            return;
        }
        if (CurrentState == PlatformState.selectable)
        {
            SelectThisPlatform();
            GameManager.Instance.AudioManagerReference?.PlaySFX(1);
        }
    }

    // Behaviours
    private void SelectThisPlatform()
    {
        PlatformClicked?.Invoke(transform.position);
    }

    private void PlatformSelectedEvent(Vector3 selectedPostion)
    {
        if (CurrentState == PlatformState.selected)
        {
            DestroyPlatform();
            return;
        }

        CheckPlatformState(selectedPostion);
    }

    private void CheckPlatformState(Vector3 selectedPosition)
    {
        if (transform.position == selectedPosition)
        {
            CurrentState = PlatformState.selected;
        }
        else if (Mathf.Abs(transform.position.x - selectedPosition.x) == 1 && transform.position.z == selectedPosition.z
                 || Mathf.Abs(transform.position.z - selectedPosition.z) == 1 && transform.position.x == selectedPosition.x)
        {
            CurrentState = PlatformState.selectable;
        }
        else
        {
            CurrentState = PlatformState.normal;
        }

        PlayCurrentAnimation();
    }

    private void PlayCurrentAnimation()
    {
        switch (CurrentState)
        {
            case PlatformState.normal:
                _animator.SetTrigger("Normal");
                break;

            case PlatformState.selected:
                _animator.SetTrigger("Pressed");
                break;

            case PlatformState.selectable:
                _animator.SetTrigger("Selected");
                avaiableMoves++;
                break;

            default:
                break;
        }
    }
    private void DestroyPlatform()
    {
        platformsCount--;
        Destroy(gameObject, 0.5f);
    }

    private void OnDestroy()
    {
        try
        {
            PlatformDestroyed();
        }
        catch
        {
            // no platform to be destroyed
        }
    }

    // Enum 
    public enum PlatformState
    {
        normal,
        selectable,
        selected,
    }
}
