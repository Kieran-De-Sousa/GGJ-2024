using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RagdollGenerator : MonoBehaviour
{
    private ControlScheme control_scheme;
    public GameObject ragdoll_prefab;

    private void Awake()
    {
        control_scheme = new ControlScheme();
        control_scheme.Ragdoll.Enable();
        control_scheme.Ragdoll.Spawn.performed += SpawnRagdoll;

        InitialSpawnRagdolls();
    }

    private void OnDisable()
    {
        control_scheme.Ragdoll.Spawn.performed -= SpawnRagdoll;
        control_scheme.Ragdoll.Disable();
    }

    private void InitialSpawnRagdolls()
    {
        for(int i = 0; i < GameData.player_count; i ++)
        {
            SpawnRagdoll(new InputAction.CallbackContext());
        }
    }

    private void SpawnRagdoll(InputAction.CallbackContext context)
    {
        Instantiate(ragdoll_prefab, new Vector3(0, 10, 0), Quaternion.identity);
    }
}
