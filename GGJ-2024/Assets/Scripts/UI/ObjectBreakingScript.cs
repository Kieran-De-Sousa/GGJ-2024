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
    private float healthMax;

    public float maxVelocityToDamage = 100;
    public float maxDamagetaken = 150;

    public float lifeSpanInSeconds = 30;
    private float lifeSpanInSecondsMax;

    private GameObject _gameController;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _gameController = GameObject.FindGameObjectWithTag("GameController");
        
        lifeSpanInSecondsMax = lifeSpanInSeconds;
        healthMax = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //return;
        }
        
        Damage(other);
    }

    private void Damage(Collider other)
    {
        var otherRigidBody = other.transform.GetComponent<Rigidbody>();
        if (_velocity >= 10 || (otherRigidBody != null && otherRigidBody.velocity.magnitude >= 10))
        {
            float damage = (_velocity / maxVelocityToDamage) * maxDamagetaken;
            if ( damage > maxDamagetaken)
            {
                damage = maxDamagetaken;
            }

            health -= damage;
            
            if (health <= 0)
            {
                DeleteObject();
                
                GameObject particle = Instantiate(objectBreakingParticle, transform.position, Quaternion.identity);
                Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                particle.transform.rotation = rotation;
                particle.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            }
        }
    }

    private void Update()
    {
        _velocity = _rigidbody.velocity.magnitude;
    }

    private void LateUpdate()
    {
        lifeSpanInSeconds -= 1 * Time.deltaTime;
        if (lifeSpanInSeconds <= 0)
        {
            DeleteObject();
        }
    }

    private void DeleteObject()
    {
        lifeSpanInSeconds = lifeSpanInSecondsMax;
        health = healthMax;
        _rigidbody.velocity = Vector3.zero;
        _gameController.GetComponent<BreakableObjectPoolScript>().ReturnObjectToPool(gameObject);
    }
}
