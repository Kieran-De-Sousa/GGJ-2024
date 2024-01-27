using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] Rigidbody myRigidbody;
    [SerializeField] GameObject rootObject;
    [SerializeField] float respawnTime = 30f;
    Collider[] allColliders;
    List<Collider> childColliders;

    void Awake()
    {
        childColliders = new List<Collider>();
        allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider ragdollCollider in allColliders)
        {
            if (ragdollCollider != myCollider)
            {
                childColliders.Add(ragdollCollider);
            }
        }

        foreach (Collider ragdollCollider in childColliders)
        {
            ragdollCollider.enabled = false;
        }

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool bIsRagdoll)
    {
        GetComponent<Animator>().enabled = !bIsRagdoll;
        myRigidbody.useGravity = !bIsRagdoll;
        myCollider.enabled = !bIsRagdoll;

        foreach (Collider ragdollCollider in childColliders)
        {
            ragdollCollider.enabled = bIsRagdoll;
        }
    }

    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(false);
        this.transform.position = rootObject.transform.position + new Vector3(0f, 0.5f, 0f);
    }

    void RandomAnimation()
    {
        int randomNum = Random.Range(0, 2);
        Debug.Log(randomNum);
        Animator animator = GetComponent<Animator>();
        if (randomNum == 0)
        {
            //animator.SetTrigger("Idle");
        }
        else
        {
            //animator.SetTrigger("Idle");
        }
    }

    public void OnRagdollEvent(Component sender, object data)
    {
        ToggleRagdoll(true);
    }
}
