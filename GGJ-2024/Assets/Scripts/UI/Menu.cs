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
    private ControlScheme controls;
    private int selected_button = 0;
    private bool in_join_mode = false;
    public int min_players = 1;
    public RectTransform arrow;
    public List<Vector2> arrow_positions;
    private bool starting = false;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.DisableJoining();

        controls = new ControlScheme();
        controls.UI.Enable();
        controls.UI.Select.performed += SelectButton;
        controls.UI.Return.performed += ReturnButton;
        controls.UI.ChangeButton.performed += ChangeButton;
    }

    private void OnDisable()
    {
        controls.UI.Select.performed -= SelectButton;
        controls.UI.Return.performed -= ReturnButton;
        controls.UI.ChangeButton.performed -= ChangeButton;
        controls.UI.Disable();
    }

    private void SelectButton(InputAction.CallbackContext context)
    {
        if (in_join_mode)
            return;
        if (selected_button == 0)
            Play();
        else if (selected_button == 1)
            Quit();
    }

    private void ReturnButton(InputAction.CallbackContext context)
    {
        if (!in_join_mode)
            return;
        ExitJoin();
    }

    private void ChangeButton(InputAction.CallbackContext context)
    {
        if (in_join_mode)
            return;
        selected_button-=(int)context.ReadValue<float>();
        // OPEN FOR THE POSSIBILITY OF MORE MENU BUTTONS
        if (selected_button > 1)
            selected_button = 0;
        else if (selected_button < 0)
            selected_button = 1;
        arrow.anchoredPosition = arrow_positions[selected_button];
    }

    private void Start()
    {
        Invoke(nameof(StartMusic), 0.25f);
    }

    private void StartMusic()
    {
        MusicPlaySettings musicPlaySettings = MusicPlaySettings.Default;
        musicPlaySettings.AudioPlaySettings.Volume = 0.25f;

        MusicManager.Instance.PlayMusicGroup(AudioGroupID.MainMenuTracks, musicPlaySettings);
    }

    /// <summary>
    /// UI reference to play button
    /// </summary>
    public void Play()
    {
        in_join_mode = true;
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
        in_join_mode = false;
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
        starting = true;
        for(int i = 0; i < 4; i ++)
        {
            if (players[i] != null)
            {
                GameData.devices[i] = players[i].devices[0];
            }
        }
        GameData.player_count = playerInputManager.playerCount;
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
        ScoreManager.Instance.ClearAllScores();
    }

    /// <summary>
    /// Condition check if players can start
    /// </summary>
    /// <returns>Bool if game can be started</returns>
    private bool StartCheck()
    {
        return (playerInputManager.playerCount >= min_players) && !starting;
    }
}
