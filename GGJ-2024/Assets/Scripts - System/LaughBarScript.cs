using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaughBarScript : MonoBehaviour
{
    private Image _barFill;

    private float _currentFillAmount;
    public float fillAmount;
    public float fillMax = 100;

    private void Awake()
    {
        _barFill = transform.GetChild(0).transform.GetComponent<Image>();
        
        fillAmount = 0;
        _currentFillAmount = fillAmount;
    }

    public void AddFill(float amount)
    {
        fillAmount += amount;

        if (fillAmount > fillMax)
        {
            fillAmount = fillMax;
        }
    }

    public void ReduceFill(float amount)
    {
        fillAmount -= amount;

        if (fillAmount < 0)
        {
            fillAmount = 0;
        }
    }

    private void Update()
    {
        if (_currentFillAmount != fillAmount)
        {
            _currentFillAmount = Mathf.Lerp(_currentFillAmount, fillAmount, 0.1f);
            _barFill.fillAmount = _currentFillAmount / fillMax;
        }
    }
}
