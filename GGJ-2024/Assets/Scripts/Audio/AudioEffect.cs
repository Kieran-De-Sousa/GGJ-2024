using UnityEngine;

/// <summary>
/// Audio data. Place in Resources->AudioEffects. See AudioManager.cs.
/// </summary>
[CreateAssetMenu(fileName = "AudioEffect", menuName = "Audio/AudioEffect", order = 1)]
public class AudioEffect : ScriptableObject
{
    public AudioID ID = AudioID.None;
    public AudioClip Clip = null;
}
