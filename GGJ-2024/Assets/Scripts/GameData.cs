using UnityEngine;
using UnityEngine.InputSystem;

public static class GameData
{
    public static InputDevice[] devices = new InputDevice[4];
    public static int player_count = 0;
    public static ScoreInfo winner;
}
