using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedicActionSlapped : ComedicActionBase
{
    [SerializeField] protected GameEvent ragdollEvent = null;
    public TestPlayerScript Initiator { get; set; }

    [SerializeField] private Collider _collider; // Slap hitbox collider
    [SerializeField] private Collider _parent; // Parent object collider

    private Collider[] allColliders;

    void Awake()
    {
        allColliders = _parent.GetComponentsInChildren<Collider>();
        foreach (Collider parentCollider in allColliders)
        {
            Physics.IgnoreCollision(_collider, parentCollider, true);
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
        Debug.Log("Collision Registered");
        if (!_collider.enabled || _collider == null) return;

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Comedy Event Raised!");
            ComedyTriggered(other.gameObject.GetComponent<Collider>());
        }
    }

    public override void ComedyTriggered(Collider collider)
    {
        if (Initiator == null && collider.CompareTag("Player"))
        {
            Initiator = _parent.GetComponent<TestPlayerScript>();
        }

        comedyEvent.Raise(Initiator, comedyAmount);
        ragdollEvent.Raise(Initiator, collider);

        PlayAudio();
    }

    private void PlayAudio()
    {
        AudioPlaySettings playSettings = AudioPlaySettings.Default;
        playSettings.Position = transform.position;
        AudioManager.Instance.PlayEffect(AudioID.Slapped, AudioMixerID.SFX, playSettings);
    }
}
