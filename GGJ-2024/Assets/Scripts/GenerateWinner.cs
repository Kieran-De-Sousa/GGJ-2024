using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GenerateWinner : MonoBehaviour
{
    private PlayerInputManager manager;
    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        GenerateWinnerCharacter();
    }

    /// <summary>
    /// Force join all players with connected devices
    /// </summary>
    private void GenerateWinnerCharacter()
    {
        PlayerInput pl = manager.JoinPlayer(0, 0, "", GameData.devices[GameData.winner.PlayerId]);
        pl.GetComponent<PlayerUIScript>().AwakeUI(GameData.winner.PlayerId);

        PlayAudio();
    }

    private void PlayAudio()
    {
        AudioPlaySettings playSettings = AudioPlaySettings.Default;
        playSettings.Volume = 100f;
        playSettings.Position = transform.position;
        AudioManager.Instance.PlayEffect(AudioID.YouWin, AudioMixerID.SFX, playSettings);
    }
}
