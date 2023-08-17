/// <summary>
/// Creates a scriptable object that stores all the sound effects
/// </summary    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sfx List")]
public class Sfx : ScriptableObject
{
    public List<SfxProperties> SfxList;
}

[System.Serializable]
public struct SfxProperties
{
    public string SfxName;
    public AudioClip SfxClip;
}