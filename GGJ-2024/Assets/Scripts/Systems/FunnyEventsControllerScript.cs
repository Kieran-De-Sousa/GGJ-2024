using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FunnyEventsControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    private GameObject _ground;

    private void Awake()
    {
        _ground = GameObject.FindGameObjectWithTag("Ground");
    }

    public void SummonRandomEvent()
    {
        GameObject newEvent = Instantiate(GetRandomPrefab(), GetRandomPosition(), quaternion.identity);
    }

    private GameObject GetRandomPrefab()
    {
        int randomInt = Random.Range(0, prefabs.Length);
        GameObject newPrefab = prefabs[randomInt];
        return newPrefab;
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-(_ground.transform.localScale.x / 2) + 5, (_ground.transform.localScale.x / 2) - 5);
        float randomZ = Random.Range(-(_ground.transform.localScale.z / 2) + 5, (_ground.transform.localScale.z / 2) - 5);

        return new Vector3(randomX, 1, randomZ);
    }
}
