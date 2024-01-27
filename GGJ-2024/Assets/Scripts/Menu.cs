using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator anim;
    private PlayerInputManager playerInputManager;
    public MenuPlayerIcon[] icons;
    private PlayerInput[] players = new PlayerInput[4];

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.DisableJoining();
    }

    /// <summary>
    /// UI reference to play button
    /// </summary>
    public void Play()
    {
        anim.SetTrigger("JoinStart");
        playerInputManager.EnableJoining();
    }

    /// <summary>
    /// UI Reference to quit button
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// UI reference to exit button
    /// </summary>
    public void ExitJoin()
    {
        anim.SetTrigger("JoinEnd");
        playerInputManager.DisableJoining();
    }

    /// <summary>
    /// Player has joined event
    /// </summary>
    /// <param name="info">PlayerInput of player that has joined</param>
    public void PlayerJoin(PlayerInput info)
    {
        if (info.playerIndex <= 3 && info.playerIndex >= 0)
        {
            icons[info.playerIndex].Join();
            players[info.playerIndex] = info;
        }
    }

    /// <summary>
    /// Player has left event
    /// </summary>
    /// <param name="info">PlayerInput of player that has left</param>
    public void PlayerLeft(PlayerInput info)
    {
        if (info.playerIndex <= 3 && info.playerIndex >= 0)
        {
            icons[info.playerIndex].Leave();
            players[info.playerIndex] = null;
        }
    }

    /// <summary>
    /// Attempt to start the game
    /// </summary>
    public void AttemptStart()
    {
        if (!StartCheck())
            return;
        
        for(int i = 0; i < 4; i ++)
        {
            if (players[i] != null)
            {
                GameData.devices[i] = players[i].devices[0];
            }
        }
        GameData.player_count = playerInputManager.playerCount;
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    /// <summary>
    /// Condition check if players can start
    /// </summary>
    /// <returns>Bool if game can be started</returns>
    private bool StartCheck()
    {
        return playerInputManager.playerCount >= 2;
    }
}
