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
            manager.JoinPlayer(i, i, "", GameData.devices[i]).transform.position = spawn_points[i];
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
