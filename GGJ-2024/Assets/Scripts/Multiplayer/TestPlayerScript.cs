using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TestPlayerScript : MonoBehaviour
{
    public float move_speed;
    private Vector2 move_vec;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        CinemachineTargetGroup targets = FindObjectOfType<CinemachineTargetGroup>();
        for (int i = 0; i < 4; i++)
        {
            if (targets.m_Targets[i].target == null)
            {
                targets.m_Targets[i].target = transform;
                break;
            }
        }
    }

    public void Move(InputAction.CallbackContext info)
    {
        move_vec = info.ReadValue<Vector2>();
    }

    public void Update()
    {
        rb.velocity = new Vector3(move_vec.x * move_speed, Mathf.Clamp(rb.velocity.y, -5, 5), move_vec.y * move_speed);
    }
}
