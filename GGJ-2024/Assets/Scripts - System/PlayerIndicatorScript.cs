using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PlayerIndicatorScript : MonoBehaviour
{
    private GameObject _player;
    private Camera _camera;
    private GameObject _canvas;
    public Vector2 offset;

    private void Awake()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        
        transform.SetParent(_canvas.transform);
        
        _camera = Camera.main;
    }

    public void SetOwner(GameObject owner)
    {
        _player = owner;

        Color playerColor = _player.transform.GetComponent<Renderer>().material.color;
        transform.GetComponent<Image>().color = playerColor;
    }

    private void Update()
    {
        Vector2 playerScreenPos = _camera.WorldToScreenPoint(_player.transform.position);
        transform.position = playerScreenPos + offset;
    }
}
