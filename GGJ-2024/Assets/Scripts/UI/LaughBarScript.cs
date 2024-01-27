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
        FillCheck();
    }

    public void ReduceFill(float amount)
    {
        fillAmount -= amount;
        FillCheck();
    }

    public void UpdateFill(float amount)
    {
        fillAmount += amount;
        FillCheck();
    }

    /// <summary>
    /// Ensure fill amount is within fill min-max values.
    /// </summary>
    /// <author>Kieran</author>
    public void FillCheck()
    {
        if (fillAmount > fillMax)
        {
            fillAmount = fillMax;
        }

        else if (fillAmount < 0)
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
