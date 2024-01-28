using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField] private bool explosive;
    [SerializeField] private float explosionForce = 50;
    [SerializeField] private float explosionRange = 10;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _gameController = GameObject.FindGameObjectWithTag("GameController");
        
        lifeSpanInSecondsMax = lifeSpanInSeconds;
        healthMax = health;
        
        SetRandomlyToExplosive();
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

        if (explosive)
        {
            if (Input.GetKeyDown("1"))
            {
                DeleteObject();
            }
        }
    }

    private void LateUpdate()
    {
        lifeSpanInSeconds -= 1 * Time.deltaTime;
        if (lifeSpanInSeconds <= 0)
        {
            DeleteObject();
        }
    }

    public void DeleteObject()
    {
        if (explosive)
        {
            Explode();
        }
        
        lifeSpanInSeconds = lifeSpanInSecondsMax;
        health = healthMax;
        _rigidbody.velocity = Vector3.zero;
        SetRandomlyToExplosive();
        _gameController.GetComponent<BreakableObjectPoolScript>().ReturnObjectToPool(gameObject);
    }

    private void SetRandomlyToExplosive()
    {
        int randomInt = Random.Range(0, 4);

        if (randomInt == 3)
        {
            explosive = true;
        }
        else
        {
            explosive = false;
        }
    }

    private void Explode()
    {
        List<GameObject> objects = new List<GameObject>();
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            objects.Add(players[i]);
        }

        List<GameObject> objectsInPool = _gameController.GetComponent<BreakableObjectPoolScript>().pool;
        for (int i = 0; i < objectsInPool.Count; i++)
        {
            if (objectsInPool[i].activeInHierarchy)
            {
                objects.Add(objectsInPool[i]);
            }
        }

        for (int i = 0; i < objects.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, objects[i].transform.position);
            if (distance <= explosionRange)
            {
                Vector3 direction = objects[i].transform.position - transform.position;
                direction.Normalize();

                float force = explosionForce * (1 - distance / explosionRange);
                
                objects[i].GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }
}
