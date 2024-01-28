using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectPoolScript : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> _pool = new List<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        GameObject[] boxesInScene = GameObject.FindGameObjectsWithTag("Box");
        for (int i = 0; i < boxesInScene.Length; i++)
        {
            _pool.Add(boxesInScene[i]);
            boxesInScene[i].transform.SetParent(transform);
        }
        
        if (_pool.Count >= poolSize)
        {
            return;
        }
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObject = Instantiate(boxPrefab, transform);
            newObject.SetActive(false);
            _pool.Add(newObject);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                _pool[i].SetActive(true);
                return _pool[i];
            }
        }

        GameObject newObject = Instantiate(boxPrefab, transform);
        _pool.Add(newObject);
        poolSize = _pool.Count;
        return newObject;
    }

    public void ReturnObjectToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
    }
}
