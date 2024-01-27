using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantRagdoll : MonoBehaviour
{
    private Transform ragdoll;

    private void Start()
    {
        ragdoll = GetComponent<Ragdoll>().transform;
        FindObjectOfType<Ragdoll>().ToggleRagdoll(true);
    }

    private void Update()
    {
        if (ragdoll.GetChild(0).GetChild(0).transform.position.y < -20)
            Destroy(gameObject);
    }
}
