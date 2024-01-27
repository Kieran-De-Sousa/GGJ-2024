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

    public void Play()
    {
        anim.SetTrigger("JoinStart");
        playerInputManager.EnableJoining();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ExitJoin()
    {
        anim.SetTrigger("JoinEnd");
        playerInputManager.DisableJoining();
    }

    public void PlayerJoin(PlayerInput info)
    {
        if (info.playerIndex <= 3 && info.playerIndex >= 0)
        {
            icons[info.playerIndex].Join();
            players[info.playerIndex] = info;
        }
        Debug.Log(info.playerIndex);
    }

    public void PlayerLeft(PlayerInput info)
    {
        if (info.playerIndex <= 3 && info.playerIndex >= 0)
        {
            icons[info.playerIndex].Leave();
            players[info.playerIndex] = null;
        }
        Debug.Log(info.playerIndex);
    }

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
        Debug.Log("STARTING");        
        
        for(int i = 0;i < 4;i ++)
            if (GameData.devices[i] != null)
                Debug.Log(GameData.devices[i]);
        Debug.Log(GameData.player_count);

        SceneManager.LoadScene("OscarScene", LoadSceneMode.Single);
    }

    private bool StartCheck()
    {
        return playerInputManager.playerCount >= 2;
    }
}
