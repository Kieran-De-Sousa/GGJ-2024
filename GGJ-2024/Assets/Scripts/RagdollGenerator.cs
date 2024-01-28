using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RagdollGenerator : MonoBehaviour
{
    private ControlScheme control_scheme;
    public GameObject ragdoll_prefab;
    public bool drop_cur_winnner;

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
            if (!drop_cur_winnner && i != GameData.winner.PlayerId || drop_cur_winnner)
                SpawnRagdoll(i);
    }

    private void SpawnRagdoll(InputAction.CallbackContext context)
    {
        int drop_index = Random.Range(0,GameData.player_count);
        if (!drop_cur_winnner && GameData.winner.PlayerId == drop_index)
            drop_index = (drop_index + Random.Range(1, GameData.player_count)) % GameData.player_count;
        SpawnRagdoll(drop_index);
    }

    private void SpawnRagdoll(int body_index)
    {
        GameObject obj = Instantiate(ragdoll_prefab, new Vector3(0, 10, 0), Quaternion.identity);
        obj.GetComponent<PlayerUIScript>().AwakeUI(body_index);
    }
}
