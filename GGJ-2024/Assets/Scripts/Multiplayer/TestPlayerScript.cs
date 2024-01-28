using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TestPlayerScript : MonoBehaviour
{
    public int PlayerIndex { get; private set; } 

    public float move_speed;

    [Header("Slapping")]
    public Collider slapHitbox;
    public float slapForce = 100;
    public ComedicActionHit slapData;
    private bool slapped = false;

    [SerializeField] private Slap _slap;

    private Vector2 move_vec;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

            slapHitbox.gameObject.SetActive(true);
            slapHitbox.enabled = true;

            Debug.Log($"Slapped = {slapHitbox.gameObject.activeSelf}");

            StopAllCoroutines();
            StartCoroutine(SlapHitboxTime());
        }
    }

    IEnumerator SlapHitboxTime()
    {
        slapped = true;
        yield return new WaitForSeconds(1.0f);

        slapped = false;
        slapHitbox.gameObject.SetActive(false);
        slapHitbox.enabled = false;
        Debug.Log($"Slapped = {slapHitbox.gameObject.activeSelf}");
    }

    public void Update()
    {
        rb.velocity = new Vector3(move_vec.x * move_speed, Mathf.Clamp(rb.velocity.y, -10, 10), move_vec.y * move_speed);
    }
}
