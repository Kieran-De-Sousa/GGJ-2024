using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private ControlScheme moveInput;
    private Vector3 moveVector = Vector3.zero;
    private Rigidbody rb = null;
    [SerializeField] private float moveSpeed = 2f;

    private void Awake()
    {
        moveInput = new ControlScheme();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveInput.Enable();
        moveInput.Gameplay.Move.performed += OnMovementPerformed;
        moveInput.Gameplay.Move.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        moveInput.Disable();
        moveInput.Gameplay.Move.performed -= OnMovementPerformed;
        moveInput.Gameplay.Move.canceled -= OnMovementCancelled;
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
