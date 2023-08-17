/// <summary>
/// Responsible for moving the avartar position and trigger animations
/// </summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterBehaviour : MonoBehaviour
{
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private Vector3 _selectedPostion;

    [SerializeField] private List<Animator> _avatarsList;
    [SerializeField] private int _selectedAvatar;

    [SerializeField] float _tweeningDuration = 0.5f;
    [SerializeField] float _tweeningDelay = 0.5f;


    // Subscribe and Unsbscribe to Event
    private void OnEnable()
    {
        PlatformBehaviour.PlatformClicked += SwitchSelectedEvent;
    }
    private void OnDisable()
    {
        PlatformBehaviour.PlatformClicked -= SwitchSelectedEvent;
    }

    private void Start()
    {
        ChooseAvatar(0);
    }

    public void ChooseAvatar(int add) 
    {
        _selectedAvatar += add;

        if (_selectedAvatar == _avatarsList.Count)
        {
            _selectedAvatar = 0;
        }

        if (_selectedAvatar == -1)
        {
            _selectedAvatar = _avatarsList.Count-1;
        }

        int i = 0;

        foreach (Animator anim in _avatarsList)
        {
            if (i == _selectedAvatar)
            {
                anim.gameObject.SetActive(true);
            }
            else
            {
                anim.gameObject.SetActive(false);
            }

            i++;
        }
    }

    private void SwitchSelectedEvent(Vector3 selectedPostion) 
    {
        _selectedPostion = selectedPostion;
        Invoke("TweenPosition", _tweeningDelay);

        _direction = (selectedPostion - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        transform.eulerAngles =  new Vector3 (0, _lookRotation.eulerAngles.y, 0);


        _avatarsList[_selectedAvatar].SetTrigger("jump");
    }

    private void TweenPosition() 
    {
        _selectedPostion = new Vector3(_selectedPostion.x, 0.05f, _selectedPostion.z);
        transform.DOLocalMove(_selectedPostion, _tweeningDuration);
    }
}
