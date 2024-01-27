using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
    public GameObject indicatorPrefab;

    private GameObject _indicator;
    public Color[] playerColor = new Color[4];

    public void AwakeUI(int index)
    {
        GetComponentInChildren<Renderer>().material.color = playerColor[index];        
        _indicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
        _indicator.GetComponent<PlayerIndicatorScript>().SetOwner(transform.gameObject);
    }
}
