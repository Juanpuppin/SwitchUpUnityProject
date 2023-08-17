/// <summary>
/// A Audion Manager, as it need to be a singleton I'm using it as a child of the GameManager, to refer to this class just use GameManager.AudioManager
/// </summary    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private Sfx _sfxList;
    [SerializeField] public Dictionary<string, AudioClip> SfxClips;

    private void Start()
    {
        _audioSource = transform.GetComponent<AudioSource>();
    }

    public void PlaySFX(int clipIndex)
    {
        _audioSource.clip = _sfxList.SfxList[clipIndex].SfxClip;
        _audioSource.Play();
    }
}
