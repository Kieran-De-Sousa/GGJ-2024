using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
    public GameObject indicatorPrefab;

    private GameObject _indicator;
    public Color playerColor;

    private void Awake()
    {
        GetComponentInChildren<Renderer>().material.color = playerColor;
        
        _indicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
        _indicator.GetComponent<PlayerIndicatorScript>().SetOwner(transform.gameObject);
    }
}
