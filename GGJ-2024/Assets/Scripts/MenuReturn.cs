using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuReturn : MonoBehaviour
{
    private ControlScheme control_scheme;

    public void Awake()
    {
        control_scheme = new ControlScheme();
        control_scheme.UI.Enable();
        control_scheme.UI.Start.performed += ReturnToMenu;
    }

    private void OnDisable()
    {
        control_scheme.UI.Start.performed -= ReturnToMenu;
        control_scheme.UI.Disable();
    }

    private void ReturnToMenu(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
