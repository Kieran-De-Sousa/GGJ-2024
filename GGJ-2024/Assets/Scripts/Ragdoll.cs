using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Collider myCollider;
    [SerializeField] Rigidbody myRigidbody;
    [SerializeField] Collider grabCollider;
    [SerializeField] GameObject rootObject;
    [SerializeField] float respawnTime = 30f;

    Collider[] allColliders;
    List<Collider> childColliders;
    public bool isRagdolling = false;

    public Animator playerAnimator;

    public GameObject cartoonEffect;

    private bool isRagdolling = false;

    void Awake()
    {
        childColliders = new List<Collider>();
        allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider ragdollCollider in allColliders)
        {
            if (ragdollCollider != myCollider && ragdollCollider.name != "SlapHitbox" && ragdollCollider != grabCollider)
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
        if (bIsRagdoll)
            Instantiate(cartoonEffect, transform.position, Quaternion.identity);
        isRagdolling = bIsRagdoll;
        GetComponent<Animator>().enabled = !bIsRagdoll;
        myRigidbody.useGravity = !bIsRagdoll;
        rootObject.GetComponent<Rigidbody>().constraints = bIsRagdoll ? RigidbodyConstraints.None : RigidbodyConstraints.FreezePositionY;
        myCollider.enabled = !bIsRagdoll;
        grabCollider.enabled = !bIsRagdoll;

        if (bIsRagdoll)
        {
            myRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            isRagdolling = true;
        }
        else
        {
            myRigidbody.constraints = RigidbodyConstraints.None;
            isRagdolling = false;
        }

        foreach (Collider ragdollCollider in childColliders)
        {
            ragdollCollider.enabled = bIsRagdoll;
        }

        AudioPlaySettings playSettings = AudioPlaySettings.Default;
        playSettings.Position = transform.position;
        AudioManager.Instance.PlayEffect(AudioID.Ragdoll, AudioMixerID.SFX, playSettings);
    }

    public void ToggleRagdollAndCoroutine(bool bIsRagdoll)
    {
        ToggleRagdoll(bIsRagdoll);
        StartCoroutine(GetBackUp());
    }

    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);

        ToggleRagdoll(false);
        playerAnimator.SetTrigger("GetUp");

        this.transform.position = rootObject.transform.position - new Vector3(0f, 1.2f, 0f);
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
        Collider temp = (Collider) data;
        if (temp.gameObject == this.gameObject)
        {
            ToggleRagdollAndCoroutine(true);
        }
    }
}
