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
    public TestPlayerScript owner { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        /*
        Collider[] parentColliders = transform.parent.GetComponentsInChildren<Collider>();
        Collider thisCollider = this.GetComponent<Collider>();

        _collider = thisCollider;

        foreach (Collider parentCollider in parentColliders)
        {
            Physics.IgnoreCollision(thisCollider, parentCollider);
        }*/
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
        if (owner != null)
        {
            comedyEvent.Raise(owner, comedyAmount);
            ragdollEvent.Raise(owner, collider);
        }
        else
        {
            if (collider.CompareTag("Player"))
            {
                Initiator = collider.GetComponent<TestPlayerScript>();
            }

            comedyEvent.Raise(Initiator, comedyAmount);
            ragdollEvent.Raise(Initiator, collider);
        }

        PlayAudio();
    }

    private void PlayAudio()
    {
        AudioPlaySettings playSettings = AudioPlaySettings.Default;
        playSettings.Position = transform.position;
        AudioManager.Instance.PlayEffect(AudioID.Slap, AudioMixerID.SFX, playSettings);
    }
}