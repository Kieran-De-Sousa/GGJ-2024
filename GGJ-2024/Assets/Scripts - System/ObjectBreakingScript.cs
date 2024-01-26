using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreakingScript : MonoBehaviour
{
    public GameObject objectBreakingParticle;

    private Rigidbody _rigidbody;
    private float _velocity;

    public float destroyVelocity = 30;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        GameObject particle = Instantiate(objectBreakingParticle, transform.position, Quaternion.identity);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        particle.transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyOnVelocity();
    }

    private void DestroyOnVelocity()
    {
        if (_velocity >= destroyVelocity)
        {
            Destroy(transform.gameObject);
        }
    }

    private void Update()
    {
        _velocity = _rigidbody.velocity.magnitude;
    }
}
