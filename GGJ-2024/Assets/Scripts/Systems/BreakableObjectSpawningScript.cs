using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakableObjectSpawningScript : MonoBehaviour
{
    [SerializeField] private GameObject breakableObjectPrefab;

    public float spawnHeight = 60;
    public float spawnDistance = 25;
    public Vector2 spawnForceRange = new Vector2(20, 40);

    private GameObject _ground;

    private void Awake()
    {
        _ground = GameObject.FindGameObjectWithTag("Ground");
    }

    public void SpawnObject(int timer)
    {
        float randomXPosition = Random.Range(-(_ground.transform.localScale.x / 2) + 1,
            (_ground.transform.localScale.x / 2) - 1);

        Vector3 spawnPoint = new Vector3(randomXPosition, spawnHeight, -spawnDistance);
        
        GameObject newObject = GetComponent<BreakableObjectPoolScript>().GetObjectFromPool();
        newObject.transform.position = spawnPoint;

        Vector3 dirToZero = new Vector3(transform.position.x * 2, transform.position.y, 0) - newObject.transform.position;
        dirToZero.Normalize();

        float spawnForce = Random.Range(spawnForceRange.x, spawnForceRange.y);
        
        newObject.GetComponent<Rigidbody>().AddForce(dirToZero * spawnForce, ForceMode.Impulse);
    }
}
