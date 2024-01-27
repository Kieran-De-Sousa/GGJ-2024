using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGenerator : MonoBehaviour
{
    public Vector3[] spawn_points = new Vector3[4];
    private PlayerInputManager manager;
    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        if (manager.joinBehavior == PlayerJoinBehavior.JoinPlayersManually)
            JoinAllPlayers();
    }

    /// <summary>
    /// Force join all players with connected devices
    /// </summary>
    private void JoinAllPlayers()
    {
        for (int i = 0; i < GameData.player_count; i++)
        {
            PlayerInput pl = manager.JoinPlayer(i, i, "", GameData.devices[i]);
            pl.transform.position = spawn_points[i];
            pl.GetComponent<PlayerUIScript>().AwakeUI(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene("LoseScreen", LoadSceneMode.Single);
        }
    }
}
