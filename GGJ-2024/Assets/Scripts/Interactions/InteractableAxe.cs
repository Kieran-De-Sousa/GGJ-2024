using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAxe : Interactable
{
    public float rotationSpeed = 360;

    public float lifeSpanInSeconds = 1;
    private void Awake()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 180);
        transform.rotation = rotation;
    }

    private void Update()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y,
            rotation.z - rotationSpeed * Time.deltaTime);

        lifeSpanInSeconds -= 1 * Time.deltaTime;

        if (lifeSpanInSeconds <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
}
