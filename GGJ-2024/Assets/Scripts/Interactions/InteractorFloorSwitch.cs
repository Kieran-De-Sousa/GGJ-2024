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

    private void OnCollisionEnter(Collision other)
    {
        // TODO: Cooldown
        if (other.collider == triggerBox)
        {
            Interact();
        }
    }
}
