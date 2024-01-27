using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TestPlayerScript : MonoBehaviour
{
    public float move_speed;
    public float rotate_speed;
    private Vector2 move_vec;
    private Rigidbody rb;
    private Vector2 rotate_vec;
    private Ragdoll ragdollScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ragdollScript = GetComponent<Ragdoll>();
        CinemachineTargetGroup targets = FindObjectOfType<CinemachineTargetGroup>();
        if (targets != null)
            UpdateTargetGroup(targets);
    }

    /// <summary>
    /// Update target group for cinemachine
    /// </summary>
    /// <param name="group"></param>
    private void UpdateTargetGroup(CinemachineTargetGroup group)
    {
        for (int i = 0; i < 4; i++)
        {
            if (group.m_Targets[i].target == null)
            {
                group.m_Targets[i].target = transform;
                return;
            }
        }
    }

    /// <summary>
    /// Update player movement direction
    /// </summary>
    /// <param name="info">CallbackContext of input information</param>
    public void Move(InputAction.CallbackContext info)
    {
        move_vec = info.ReadValue<Vector2>();
    }

    public void Rotate(InputAction.CallbackContext info)
    {
        rotate_vec = info.ReadValue<Vector2>();
    }

    public void Update()
    {
        //Debug.Log(rotate_vec);
        if (!ragdollScript.isRagdolling)
        {
            rb.velocity = new Vector3(move_vec.x * move_speed, Mathf.Clamp(rb.velocity.y, -10, 10), move_vec.y * move_speed);

            float rotationAngle = Mathf.Atan2(rotate_vec.x, rotate_vec.y) * Mathf.Rad2Deg;
            //Debug.Log(rotationAngle);
            transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f) * Quaternion.identity;
        }
    }
}
