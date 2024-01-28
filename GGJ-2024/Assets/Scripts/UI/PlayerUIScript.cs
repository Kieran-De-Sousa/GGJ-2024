using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
    public GameObject indicatorPrefab;
    public Renderer target_renderer;
    public TrailRenderer trail_renderer;
    private GameObject _indicator;
    public Color[] playerColor = new Color[4];

    public void AwakeUI(int index)
    {
        target_renderer.material.color = playerColor[index];
        trail_renderer.startColor = playerColor[index];
        trail_renderer.endColor = DesaturateColour(playerColor[index],0.2f);
        _indicator = Instantiate(indicatorPrefab, transform.position, Quaternion.identity);
        _indicator.GetComponent<PlayerIndicatorScript>().SetOwner(transform.gameObject);
    }

    private Color DesaturateColour(Color inp, float sat_perc)
    {
        Color.RGBToHSV(inp, out float hue, out float sat, out float val);
        Color output = Color.HSVToRGB(hue, sat * sat_perc, val);
        return output;
    }
}
