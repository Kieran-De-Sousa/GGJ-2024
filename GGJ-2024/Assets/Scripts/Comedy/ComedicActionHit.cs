using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedicActionHit : ComedicActionBase
{
    [SerializeField] protected GameEvent ragdollEvent = null;

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

    public override void ComedyTriggered(Collider collider)
    {
        if (!comedyTriggered)
        {
            comedyTriggered = true;
            comedyEvent.Raise(this, comedyAmount);
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
