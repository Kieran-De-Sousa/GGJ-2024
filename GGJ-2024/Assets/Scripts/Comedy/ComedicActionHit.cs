using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedicActionHit : ComedicActionBase
{
    [SerializeField] protected GameEvent ragdollEvent = null;
    public TestPlayerScript Initiator { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
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
            ComedyTriggered(other.gameObject.GetComponent<Collider>());
        }
    }

    public override void ComedyTriggered(Collider collider)
    {
        if (!comedyTriggered)
        {
            comedyTriggered = true;
            if (Initiator == null && collider.CompareTag("Player"))
            {
                Initiator = collider.GetComponent<TestPlayerScript>();
            }
            comedyEvent.Raise(Initiator, comedyAmount);
            ragdollEvent.Raise(this, collider);

            PlayAudio();

        }
    }

    private void PlayAudio()
    {
        AudioPlaySettings playSettings = AudioPlaySettings.Default;
        playSettings.Position = transform.position;
        AudioManager.Instance.PlayEffect(AudioID.Slap, AudioMixerID.SFX, playSettings);
    }
}
