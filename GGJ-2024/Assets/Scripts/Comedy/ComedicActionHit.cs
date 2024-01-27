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
            ComedyTriggered();
        }
    }

    public override void ComedyTriggered()
    {
        if (!comedyTriggered)
        {
            comedyTriggered = true;
            comedyEvent.Raise(this, comedyAmount);
            ragdollEvent.Raise(this, 0);
        }
    }
}
