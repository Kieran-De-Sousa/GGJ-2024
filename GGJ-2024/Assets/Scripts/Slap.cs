using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slap : MonoBehaviour
{
    [Header("Slapping")]
    public Collider slapHitbox;
    public float slapForce = 100;
    public ComedicActionHit slapData;
    private bool slapped = false;

    public void SlapEngaged()
    {
        if (slapped) return;

        if (!GetComponent<Animator>().GetBool("Slap"))
        {
            GetComponentInParent<Animator>().SetTrigger("Slap");

            slapHitbox.gameObject.SetActive(true);
            slapHitbox.enabled = true;

            Debug.Log($"Slapped = {slapHitbox.gameObject.activeSelf}");

            StopAllCoroutines();
            StartCoroutine(SlapHitboxTime());
        }
    }

    IEnumerator SlapHitboxTime()
    {
        //slapped = true;
        yield return new WaitForSeconds(1.0f);

        slapped = false;
        slapHitbox.gameObject.SetActive(false);
        slapHitbox.enabled = false;

        Debug.Log($"Slapped = {slapHitbox.gameObject.activeSelf}");
    }
}
