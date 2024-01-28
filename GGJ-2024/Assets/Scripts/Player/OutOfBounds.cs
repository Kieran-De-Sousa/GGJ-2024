using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private Ragdoll ragdollScript = null;
    [SerializeField] PlayerGenerator playerGeneratorScript = null;
    Transform[] allTransforms;
    List<Transform> allChildTransforms;

    private void Awake()
    {
        ragdollScript = GetComponent<Ragdoll>();
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
        transform.position = new Vector3(0f, 2.0f, 0);
        Debug.Log("respawn");
    }
}
