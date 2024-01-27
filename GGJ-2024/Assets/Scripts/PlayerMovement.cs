using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private ControlScheme input;

    private void Awake()
    {
        input = new ControlScheme();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Gameplay.Move.performed += OnMovementPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Gameplay.Move.performed -= OnMovementPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {

    }
}
