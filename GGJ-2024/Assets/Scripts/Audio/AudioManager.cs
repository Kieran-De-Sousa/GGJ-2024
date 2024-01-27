using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public struct AudioPlaySettings
{
    public static AudioPlaySettings Default = new(1.0f, 1.0f, false, null);

    public float Volume;
    public float Pitch;
    public bool Loop;
    public Vector3? Position;

    public AudioPlaySettings(float volume, float pitch, bool loop, Vector3? position)
    {
        Volume = volume;
        Pitch = pitch;
        Loop = loop;
        Position = position;
    }
}

public enum AudioMixerID : uint
{
    Music,
    SFX
};

/// <summary>
/// All audio is routed through this class.
/// - Play an audio affect like: AudioManager.Instance.PlayEffect(AudioID.Test, AudioMixerID.SFX, AudioPlaySettings.Default);
/// - Stop an audio effect like: AudioManager.Instance.StopVoices(AudioManager.Instance.GetAllVoices(AudioMixerID.SFX));
/// - Use GetAudioMixer/Effect to change stuff at runtime. Eg, the settings might grab an audio mixer to change the volume.
/// - See AudioEffect.cs
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer _mainMixer;
    [SerializeField] private uint _voicePoolSize;

    private Dictionary<AudioMixerID, AudioMixerGroup> _audioMixers;
    private Dictionary<AudioID, AudioEffect> _audioEffects;
    private AudioSource[] _voicePool;

    private const string _audioSFXResourcePath = "Audio/Effects";
    private const string _audioMusicResourcePath = "Audio/Music";
    private const string _musicMixerName = "Music";
    private const string _sfxMixerName = "SFX";

    private AudioManager()
    {
        _mainMixer = null;
        _voicePoolSize = 3u;

        _audioMixers = new Dictionary<AudioMixerID, AudioMixerGroup>();
        _audioEffects = new Dictionary<AudioID, AudioEffect>();
        _voicePool = null;
    }

    protected override void Awake()
    {
        base.Awake();

        InitAudioMixers(_musicMixerName, _sfxMixerName);
        InitVoicePool(_voicePoolSize);
        InitAudioEffects(_audioSFXResourcePath);
        InitAudioEffects(_audioMusicResourcePath);
    }

    public AudioSource PlayEffect(AudioID audioID, AudioMixerID mixerID, AudioPlaySettings playSettings, AudioSource existingSource = null)
    {
        if (audioID == AudioID.None)
        {
            // This only logs a warning because it might just be placeholder behavior
            Debug.LogWarning($"Called {nameof(PlayEffect)} passing in AudioID.None");
            return null;
        }

        AudioMixerGroup mixer = GetMixer(mixerID);
        AudioEffect effect = GetEffect(audioID);
        AudioSource voice = existingSource ? existingSource : GetAvailableVoice();

        if (mixer && effect && voice)
        {
            SetAudioVoiceForEffect(voice, mixer, effect, playSettings);
            voice.Play();
        }

        return voice;
    }

    public void StopVoice(AudioSource voice)
    {
        if (voice)
        {
            voice.Stop();
        }
    }

    public void StopVoices(IEnumerable<AudioSource> voices)
    {
        foreach (AudioSource voice in voices)
        {
            StopVoice(voice);
        }
    }

    public IEnumerable<AudioSource> GetAllVoices(AudioID id)
    {
        AudioEffect effect = GetEffect(id);
        return GetAudioVoicesHelper(effect, (v => v.clip == effect.Clip));
    }

    public IEnumerable<AudioSource> GetAllVoices(AudioMixerID id)
    {
        AudioMixerGroup mixer = GetMixer(id);
        return GetAudioVoicesHelper(mixer, (v => v.outputAudioMixerGroup == mixer));
    }

    public AudioMixerGroup GetMixer(AudioMixerID id)
    {
        return GetAudioAssetHelper(id, _audioMixers);
    }

    public AudioEffect GetEffect(AudioID id)
    {
        return GetAudioAssetHelper(id, _audioEffects);
    }

    public bool IsVoice3D(AudioSource voice)
    {
        return voice.spatialBlend > float.Epsilon;
    }

    private void InitVoicePool(uint poolSize)
    {
        _voicePool = new AudioSource[poolSize];

        for (uint i = 0u; i < poolSize; ++i)
        {
            var voice = new GameObject($"Voice{i}");
            voice.transform.parent = transform;
            //voice.tag = Tags.AudioVoice;
            _voicePool[i] = voice.AddComponent<AudioSource>();
            SphereCollider col = voice.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.2f;
            Rigidbody rb = voice.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    private void InitAudioEffects(string path)
    {
        AudioEffect[] audioEffects = Resources.LoadAll<AudioEffect>(path);
        foreach (AudioEffect audioEffect in audioEffects)
        {
            Debug.Assert(audioEffect.ID != AudioID.None, $"Loading an audio effect \"{audioEffect.name}\" which doesn't have a valid ID");
            Debug.Assert(audioEffect.Clip, $"Loading an audio effect \"{audioEffect.name}\" which doesn't have a valid clip");

            if (!_audioEffects.ContainsKey(audioEffect.ID))
            {
                _audioEffects.Add(audioEffect.ID, audioEffect);
                Debug.Log($"Loaded audio effect with ID \"{audioEffect.ID}\". Audio effects loaded: {_audioEffects.Count}");
            }
            else
            {
                Debug.Assert(false, $"Trying to load an audio effect \"{audioEffect.name}\" with ID \"{audioEffect.ID}\" which already exists");
            }
        }
    }

    private void InitAudioMixers(string musicMixerName, string sfxMixerName)
    {
        void InitAudioMixersHelper(AudioMixerID id, string mixerName)
        {
            AudioMixerGroup mixer = _mainMixer.FindMatchingGroups(mixerName).FirstOrDefault();
            if (mixer)
            {
                _audioMixers.Add(id, mixer);
            }
            else
            {
                Debug.Assert(false, $"Couldn't find a music mixer with name \"{mixerName}\"");
            }
        }

        InitAudioMixersHelper(AudioMixerID.Music, musicMixerName);
        InitAudioMixersHelper(AudioMixerID.SFX, sfxMixerName);
    }

    private AudioSource GetAvailableVoice()
    {
        AudioSource voice = null;
        voice = _voicePool.Where(v => !v.isPlaying).FirstOrDefault();
        //Debug.Assert(false, $"Couldn't find an available voice. Consider increasing the pool size?");
        return voice;
    }

    private void SetAudioVoiceForEffect(AudioSource voice, AudioMixerGroup mixer, AudioEffect effect, AudioPlaySettings playSettings)
    {
        voice.outputAudioMixerGroup = mixer;
        voice.clip = effect.Clip;

        voice.volume = playSettings.Volume;
        voice.pitch = playSettings.Pitch;
        voice.loop = playSettings.Loop;

        if (playSettings.Position.HasValue)
        {
            const float spatialBlend3D = 1.0f;
            voice.spatialBlend = spatialBlend3D;
            voice.transform.position = playSettings.Position.Value;
        }
        else
        {
            const float spatialBlend2D = 0.0f;
            voice.spatialBlend = spatialBlend2D;
        }
    }

    private IEnumerable<AudioSource> GetAudioVoicesHelper<T>(T comparisonType, Func<AudioSource, bool> findFunc) where T : class
    {
        IEnumerable<AudioSource> voices = null;

        if (comparisonType != null)
        {
            voices = _voicePool.Where(findFunc);
        }

        return voices;
    }

    private U GetAudioAssetHelper<T, U>(T id, Dictionary<T, U> dictionary) where T : Enum /*,*/ where U : class
    {
        U idType = null;

        if (dictionary.ContainsKey(id))
        {
            idType = dictionary[id];
        }
        else
        {
            Debug.Assert(false, $"Couldn't find an audio type with ID \"{id}\"");
        }

        return idType;
    }
}
