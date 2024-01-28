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

    private bool _filledMax = false;

    private void Awake()
    {
        _barFill = transform.GetChild(1).transform.GetComponent<Image>();
        
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

    public void UpdateFill(Component sender, object amount)
    {
        if (amount is float value)
        {
            fillAmount += value;
            FillCheck();

            // TODO: ADD AUDIENCE LAUGHTER
        }
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

        if (fillAmount == fillMax && _filledMax == false)
        {
            AudioPlaySettings playSettings = AudioPlaySettings.Default;
            playSettings.Position = transform.position;
            AudioManager.Instance.PlayEffect(AudioID.KingLaugh01, AudioMixerID.SFX, playSettings);

            _filledMax = true;
        }
    }
}
