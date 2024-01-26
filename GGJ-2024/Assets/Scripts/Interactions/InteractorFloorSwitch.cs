using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorFloorSwitch : InteractorBase
{
    public BoxCollider triggerBox;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

     void OnCollisionEnter(Collision other)
     {

    }

     private void OnTriggerEnter(Collider other)
     {
         // TODO: Cooldown
         Debug.Log("Collision Detected");
         Interact();
     }
}
