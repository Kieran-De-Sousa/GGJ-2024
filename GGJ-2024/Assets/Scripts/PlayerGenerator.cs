using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void JoinAllPlayers()
    {
        Debug.Log(GameData.player_count);
        for (int i = 0; i < GameData.player_count; i++)
        {
            manager.JoinPlayer(i, i, "Control Scheme", GameData.devices[i]).transform.position = spawn_points[i];
        }
    }
}
