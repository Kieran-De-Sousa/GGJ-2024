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

    public override void ComedyTriggered(Collider collider)
    {
        if (!comedyTriggered)
        {
            comedyTriggered = true;
            comedyEvent.Raise(Initiator, comedyAmount);
            ragdollEvent.Raise(this, collider);
        }
    }
}
