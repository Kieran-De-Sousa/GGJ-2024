using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectPoolScript : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int poolSize = 10;

    public List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        GameObject[] boxesInScene = GameObject.FindGameObjectsWithTag("Box");
        for (int i = 0; i < boxesInScene.Length; i++)
        {
            pool.Add(boxesInScene[i]);
            boxesInScene[i].transform.SetParent(transform);
        }
        
        if (pool.Count >= poolSize)
        {
            return;
        }
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObject = Instantiate(boxPrefab, transform);
            newObject.SetActive(false);
            pool.Add(newObject);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject newObject = Instantiate(boxPrefab, transform);
        pool.Add(newObject);
        poolSize = pool.Count;
        return newObject;
    }

    public void ReturnObjectToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
    }
}
