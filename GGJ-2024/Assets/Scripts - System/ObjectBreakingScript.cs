using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreakingScript : MonoBehaviour
{
    [SerializeField] private GameObject objectBreakingParticle;

    private Rigidbody _rigidbody;
    private float _velocity;

    public float health = 100;

    public float maxVelocityToDamage = 100;
    public float maxDamagetaken = 150;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Damage();
    }

    private void Damage()
    {
        if (_velocity >= 10)
        {
            float damage = (_velocity / maxVelocityToDamage) * maxDamagetaken;
            if ( damage > maxDamagetaken)
            {
                damage = maxDamagetaken;
            }

            health -= damage;
            
            if (health <= 0)
            {
                Destroy(gameObject);
                
                GameObject particle = Instantiate(objectBreakingParticle, transform.position, Quaternion.identity);
                Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                particle.transform.rotation = rotation;
            }
        }
    }

    private void Update()
    {
        _velocity = _rigidbody.velocity.magnitude;
        
        //should be removed
        health -= 2 * Time.deltaTime;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
