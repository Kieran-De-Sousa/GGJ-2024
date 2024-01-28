using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OutOfBounds : MonoBehaviour
{
    private Ragdoll ragdollScript = null;
    [SerializeField] PlayerGenerator playerGeneratorScript = null;
    Transform[] allTransforms;
    List<Transform> allChildTransforms;
    private PlayerInput input = null;

    private void Awake()
    {
        playerGeneratorScript = GameObject.Find("PlayerInputManager").GetComponent<PlayerGenerator>();
        ragdollScript = GetComponent<Ragdoll>();
        input = GetComponent<PlayerInput>();
        allTransforms = GetComponentsInChildren<Transform>();
        allChildTransforms = new List<Transform>();
        foreach (Transform t in allTransforms) 
        {
            if (t != transform)
            {
                allChildTransforms.Add(t);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform transform in allChildTransforms)
        {
            if (transform.position.y < -1.3 || transform.position.y > 15.2)
            {
                RespawnPlayer();
            }

            if (transform.position.x < - 21 ||  transform.position.x > 21)
            {
                RespawnPlayer();
            }

            if (transform.position.z < -10.2 || transform.position.z > 10.2)
            {
                RespawnPlayer();
            }
        }
    }

    private void RespawnPlayer()
    {
        ragdollScript.ToggleRagdoll(false);
        transform.position = playerGeneratorScript.spawn_points[input.playerIndex];
        Debug.Log("respawn");
    }
}
