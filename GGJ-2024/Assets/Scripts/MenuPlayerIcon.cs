using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuPlayerIcon : MonoBehaviour
{
    public Image a_button;
    public Image p_icon;

    private void Awake()
    {
        a_button.enabled = true;
        p_icon.enabled = false;
    }

    /// <summary>
    /// Update images with joined player
    /// </summary>
    public void Join()
    {
        a_button.enabled = false;
        p_icon.enabled = true;
    }

    /// <summary>
    /// Update images with left player
    /// </summary>
    public void Leave()
    {
        a_button.enabled = true;
        p_icon.enabled = false;
    }
}
