using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakableObjectSpawningScript : MonoBehaviour
{
    [SerializeField] private GameObject breakableObjectPrefab;

    public float spawnHeight = 25;
    public float spawnRadius = 70;
    public Vector2 spawnForceRange = new Vector2(30, 70);
    
    public void SpawnObject(int timer)
    {
        float angle = (timer % 60) * 360;
        
        Vector3 spawnPoint = new Vector3(spawnRadius * Mathf.Cos(angle), spawnHeight, spawnRadius * Mathf.Sin(angle));

        GameObject newObject = Instantiate(breakableObjectPrefab, transform.position, Quaternion.identity);
        newObject.transform.position = spawnPoint;

        Vector3 dirToZero = new Vector3(0, 0, 0) - newObject.transform.position;
        dirToZero.Normalize();

        float spawnForce = Random.Range(spawnForceRange.x, spawnForceRange.y);
        
        newObject.GetComponent<Rigidbody>().AddForce(dirToZero * spawnForce, ForceMode.Impulse);
    }
}
