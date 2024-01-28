using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAxe : Interactable
{
    [SerializeField] private float rotationSpeed = 360;

    [SerializeField] private float lifeSpanInSeconds = 5;

    [SerializeField] private Vector2 rotationEulerAnglesRange = new Vector2(0, 240);
    [SerializeField] private float rotationEulerAngle;

    [SerializeField] private float heightOnSpawn = 35;
    [SerializeField] private float heightOnOscillation = 25;

    public bool _stopping;
    private void Awake()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, rotationEulerAngle);
        transform.rotation = rotation;

        transform.position = new Vector3(transform.position.x, heightOnSpawn, transform.position.z);
    }

    private void Update()
    {
        if (transform.position.y > heightOnOscillation && !_stopping)
        {
            transform.position = new Vector3(transform.position.x,
                transform.position.y - (heightOnSpawn - heightOnOscillation) * Time.deltaTime,
                transform.position.z);
        }

        if (_stopping)
        {
            transform.position = new Vector3(transform.position.x,
                transform.position.y + (heightOnSpawn - heightOnOscillation) * Time.deltaTime, 
                transform.position.z);
        }
        
        rotationEulerAngle = Mathf.SmoothStep(rotationEulerAnglesRange.x, rotationEulerAnglesRange.y,
            Mathf.PingPong(Time.time * 1, 1));
        
        Vector3 rotation = transform.rotation.eulerAngles;
        
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y,
            rotationEulerAngle);

        lifeSpanInSeconds -= 1 * Time.deltaTime;

        if (lifeSpanInSeconds <= 1)
        {
            _stopping = true;
        }

        if (lifeSpanInSeconds <= 0)
        {
            Destroy(gameObject);
        }
    }

    /*
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("esfhiueshuiyefihusef");
            var actionHit = GetComponent<ComedicActionHit>();
            actionHit.Initiator = other.gameObject.GetComponent<TestPlayerScript>();
            actionHit.ComedyTriggered(other.gameObject.GetComponent<Collider>());
        }
    }
    */

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
}
