using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public void PlayerJoin(PlayerInput info)
    {
        Debug.Log(info.playerIndex);
    }

    public void PlayerLeft(PlayerInput info)
    {
        Debug.Log(info.playerIndex);
    }
}
