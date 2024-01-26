using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerScript : MonoBehaviour
{
    public float move_speed;
    private Vector2 move_vec;

    public void Move(InputAction.CallbackContext info)
    {
        move_vec = info.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        transform.position += new Vector3(move_vec.x, 0, move_vec.y) * move_speed * Time.deltaTime;
    }
}
