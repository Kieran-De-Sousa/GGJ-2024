using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComedicActionHit : ComedicActionBase
{
    [SerializeField] protected GameEvent ragdollEvent = null;
    public TestPlayerScript Initiator { get; set; }
    private Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        Collider[] parentColliders = transform.parent.GetComponentsInChildren<Collider>();
        Collider thisCollider = this.GetComponent<Collider>();

        _collider = thisCollider;

        foreach (Collider parentCollider in parentColliders)
        {
            Physics.IgnoreCollision(thisCollider, parentCollider);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Comedy Event Raised!");
            ComedyTriggered(other);
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        // Find the Collider component in the current GameObject and its children
        Collider ownCollider = GetComponentInChildren<Collider>();

        if (ownCollider != null && !ownCollider.enabled)
        {
            // If the collider is not enabled, return without further processing
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Comedy Event Raised!");
            Debug.Log(
                $"Other Collider: {other.gameObject.name} ---- This == {this.gameObject.GetComponent<Collider>()}");
            ComedyTriggered(other.gameObject.GetComponent<Collider>());
        }
    }

    public override void ComedyTriggered(Collider collider)
    {
        if (Initiator == null && collider.CompareTag("Player"))
        {
            Initiator = collider.GetComponent<TestPlayerScript>();
        }
        comedyEvent.Raise(Initiator, comedyAmount);
        ragdollEvent.Raise(this, collider);

        PlayAudio();
    }

    private void PlayAudio()
    {
        AudioPlaySettings playSettings = AudioPlaySettings.Default;
        playSettings.Position = transform.position;
        AudioManager.Instance.PlayEffect(AudioID.Slap, AudioMixerID.SFX, playSettings);
    }
}

public static class TramsjfiriExtenstions
{
    public static Transform ULTIMATE_PARENT(this Transform transform)
    {
        Transform ultimateParent = transform;
        while (ultimateParent.parent != null)
        {
            ultimateParent = ultimateParent.parent;
        }

        Debug.Log($"Ultimate Parent: {ultimateParent.GetComponent<PlayerInput>().playerIndex}");

        return ultimateParent;
    }
}

public static class CollisionExtenstions
{
    // yeah?
    public static bool ULTIMATE_COLLISION_CHECK(this Transform transform, Collider other)
    {
        Transform topLevel = transform.ULTIMATE_PARENT();
        List<Collider> colliders = topLevel.GetComponentsInChildren<Collider>().ToList();
        if (transform.GetComponent<Collider>())
        {
            if (!colliders.Contains(transform.GetComponent<Collider>()))
            {
                colliders.Add(transform.GetComponent<Collider>());
            }
        }

        bool result = false;
        foreach (var col in colliders)
        {
            if (col == other) result = true;
        }

        return result;
    }
}
