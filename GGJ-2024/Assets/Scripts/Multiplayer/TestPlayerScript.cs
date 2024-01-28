using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TestPlayerScript : MonoBehaviour
{
    public int PlayerIndex { get; private set; } 

    public float move_speed;
    public float rotate_speed;

    [Header("Slapping")]
    public Collider slapHitbox;
    public float slapForce = 100;
    private bool slapped = false;

    [SerializeField] private Slap _slap;

    private Vector2 move_vec;
    private Rigidbody rb;
    private Vector2 rotate_vec;
    private Ragdoll ragdollScript;
    private Collider grabbableCollider = null;
    private Collider currentGrabbable = null;
    private bool holdingObject = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ragdollScript = GetComponent<Ragdoll>();
        CinemachineTargetGroup targets = FindObjectOfType<CinemachineTargetGroup>();
        if (targets != null)
            UpdateTargetGroup(targets);

        PlayerIndex = GetComponent<PlayerInput>().playerIndex;
    }

    /// <summary>
    /// Update target group for cinemachine
    /// </summary>
    /// <param name="group"></param>
    private void UpdateTargetGroup(CinemachineTargetGroup group)
    {
        for (int i = 0; i < 4; i++)
        {
            if (group.m_Targets[i].target == null)
            {
                group.m_Targets[i].target = transform.GetChild(0).GetChild(0);
                return;
            }
        }
    }

    /// <summary>
    /// Update player movement direction
    /// </summary>
    /// <param name="info">CallbackContext of input information</param>
    public void Move(InputAction.CallbackContext info)
    {
        move_vec = info.ReadValue<Vector2>();
    }

    public void Rotate(InputAction.CallbackContext info)
    {
        rotate_vec = info.ReadValue<Vector2>();
        //Debug.Log(rotate_vec);
    }

    public void Slap(InputAction.CallbackContext info)
    {
        // TODO: Please fix this I have spent 6 hours trying to let us slap someone
        // TODO: and the stupid box collider attached to this parent hits the idiots collider
        // TODO: instead of our magical capsule collider............ I am mad - Kieran.
        //_slap.SlapEngaged();

        if (slapped) return;

        if (!GetComponent<Animator>().GetBool("Slap"))
        {
            GetComponent<Animator>().SetTrigger("Slap");

            slapped = true;
            slapHitbox.enabled = true;

            Debug.Log($"Slapped Hitbox = {slapHitbox.enabled}");

            StopAllCoroutines();
            StartCoroutine(SlapHitboxTime());
        }
    }

    public void Grab(InputAction.CallbackContext info)
    {
        if(!holdingObject && grabbableCollider != null)
        {
            currentGrabbable = grabbableCollider;
            holdingObject = true;
            currentGrabbable.gameObject.GetComponent<Rigidbody>().useGravity = false;
            currentGrabbable.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentGrabbable.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            if (currentGrabbable.gameObject.tag == "Player")
            {
                currentGrabbable.gameObject.GetComponent<Ragdoll>().ToggleRagdoll(true);
            }
            currentGrabbable.gameObject.transform.parent = transform;
            currentGrabbable.gameObject.transform.position =
                new Vector3(
                    transform.position.x,
                    transform.position.y + (3 * (ragdollScript.myCollider.bounds.size.y/4)),
                    transform.position.z + (ragdollScript.myCollider.bounds.size.z / 2) + (currentGrabbable.bounds.size.z / 2)
                    );
        }
    }

    public void Throw(InputAction.CallbackContext info)
    {
        if (holdingObject) 
        {
            LostGrabbable();
            currentGrabbable.gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, 100f);
        }
    }

    private void LostGrabbable()
    {
        currentGrabbable.gameObject.GetComponent<Rigidbody>().useGravity = true;
        currentGrabbable.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        currentGrabbable.gameObject.transform.parent = null;
        holdingObject = false;
        if (currentGrabbable.gameObject.tag == "Player")
        {
            currentGrabbable.gameObject.GetComponent<Ragdoll>().ToggleRagdollAndCoroutine(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box" || other.tag == "Player")
        {
            if (!holdingObject)
            {
                grabbableCollider = other;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box" || other.tag == "Player")
        {
            grabbableCollider = null;
        }
    }

    IEnumerator SlapHitboxTime()
    {
        yield return new WaitForSeconds(1.0f);

        slapped = false;
        slapHitbox.enabled = false;
        Debug.Log($"Slapped Hitbox = {slapHitbox.enabled}");
    }

    public void Update()
    {
        if (!ragdollScript.isRagdolling)
        {
            rb.velocity = new Vector3(move_vec.x * move_speed, Mathf.Clamp(rb.velocity.y, -10, 10), move_vec.y * move_speed);

            float rotationAngle = Mathf.Atan2(rotate_vec.x, rotate_vec.y) * Mathf.Rad2Deg;
            if (rotationAngle != 0)
            {
                transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f) * Quaternion.identity;
            }   
        }

        if (ragdollScript.isRagdolling && holdingObject)
        {
            LostGrabbable();
        }
    }
}
