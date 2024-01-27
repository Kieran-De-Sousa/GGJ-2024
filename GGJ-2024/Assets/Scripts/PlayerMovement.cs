using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private ControlScheme input;
    private Vector3 moveVector = Vector3.zero;
    private Rigidbody rb = null;
    [SerializeField] private float moveSpeed = 2f;

    private void Awake()
    {
        input = new ControlScheme();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Gameplay.Move.performed += OnMovementPerformed;
        input.Gameplay.Move.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Gameplay.Move.performed -= OnMovementPerformed;
        input.Gameplay.Move.canceled -= OnMovementCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector3.zero;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveVector.x * moveSpeed, Mathf.Clamp(rb.velocity.y, -10, 10), moveVector.y * moveSpeed);
        //Debug.Log(moveVector);
    }
}
