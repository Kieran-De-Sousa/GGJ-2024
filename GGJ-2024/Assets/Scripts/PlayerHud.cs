using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public Image icon;
    public Color[] colors = new Color[4];
    public TextMeshProUGUI score;

    public void Init(int index)
    {
        icon.color = colors[index];
        score.text = "0";
    }

    public void UpdateHud(float scr)
    {
        score.text = scr.ToString();
    }
}
