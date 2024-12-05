using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireColorChanger : MonoBehaviour
{
    private NewWire wire;
    private LineRenderer lineRenderer;
    private void Awake()
    {
        wire = GetComponent<NewWire>();
        lineRenderer = GetComponent<LineRenderer>();
        wire.ValueChanged += ChangeColor;
    }
    private void OnDisable()
    {
        wire.ValueChanged -= ChangeColor;
    }
    private void ChangeColor(object sender,EventArgs e)
    {
        if (wire.Type == ValueType.BYTE)
        {
            if(wire.Value == -1)
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
            else
            {
                lineRenderer.startColor = Color.blue;
                lineRenderer.endColor = Color.blue;
            }
            return;
        }

        if (wire.Value == 0)
        {
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }
        else if(wire.Value==1)
        {
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
        //¶ÌÂ·±äºìÉ«
        else if (wire.Value == -1)
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
    }
}
