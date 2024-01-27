using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A group of audio effects. See AudioGroupID.cs
/// </summary>
[CreateAssetMenu(fileName = "AudioEffectGroup", menuName = "Audio/AudioEffectGroup", order = 2)]
public class AudioEffectGroup : ScriptableObject
{
    public AudioGroupID ID = AudioGroupID.None;
    public List<AudioID> Effects = new();
}
