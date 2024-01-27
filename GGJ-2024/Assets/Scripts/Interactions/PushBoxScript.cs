using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxScript : MonoBehaviour
{
    [SerializeField] private float speedMax = 200;
    private float _currentSpeed;

    private float _maxReach;

    private Vector3 _spawnPosition;

    private float _delayTimer = 1;

    private bool _speeding = true;
    private bool _retracting;

    private void Awake()
    {
        _maxReach = transform.GetChild(0).transform.localScale.y;

        _spawnPosition = transform.position;
    }

    private void Update()
    {
        if (_speeding)
        {
            _currentSpeed += 300 * Time.deltaTime;
            if (_currentSpeed > speedMax)
            {
                _currentSpeed = speedMax;
            }
        }
        
        if (transform.position.y <= _spawnPosition.y + _maxReach && !_retracting)
        {
            transform.position = new Vector3(transform.position.x,
                transform.position.y + _currentSpeed * Time.deltaTime, transform.position.z);
        }
        else
        {
            _retracting = true;
            _speeding = false;
        }

        if (_retracting)
        {
            _delayTimer -= 1 * Time.deltaTime;
            if (_delayTimer <= 0)
            {
                _speeding = true;

                transform.position = new Vector3(transform.position.x,
                    transform.position.y - _currentSpeed / 20 * Time.deltaTime, transform.position.z);

                if (transform.position.y <= _spawnPosition.y)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
