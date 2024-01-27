using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    public float comedyValue = 0;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUpdateComedyValue(Component sender, object comedy)
    {
        if (comedy is float)
        {
            float value = (float) comedy;
            comedyValue += (float) comedy;
        }
    }
}
