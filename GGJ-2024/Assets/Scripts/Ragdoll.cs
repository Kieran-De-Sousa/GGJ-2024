using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;
    bool bIsRagdoll = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!bIsRagdoll && collision.gameObject.tag == "Projectile")
        {
            ToggleRagdoll(false);
            StartCoroutine(GetBackUp());
        }
    }

    private void ToggleRagdoll(bool bisAnimating)
    {
        bIsRagdoll = !bisAnimating;

        myCollider.enabled = bisAnimating;
        foreach (Rigidbody ragdollBone in rigidbodies)
        {
            //ragdollBone.isKinematic = bisAnimating;
        }

        GetComponent<Animator>().enabled = bisAnimating;
        if(bisAnimating)
        {
            //RandomAnimation();
        }
    }

    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
