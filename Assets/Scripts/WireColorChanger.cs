using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireColorChanger : MonoBehaviour
{
    private Wire wire;
    private LineRenderer lineRenderer;
    private void Awake()
    {
        wire = GetComponent<Wire>();
        lineRenderer = GetComponent<LineRenderer>();
        wire.ValueChanged += ChangeColor;
    }
    private void OnDisable()
    {
        wire.ValueChanged -= ChangeColor;
    }
    private void ChangeColor()
    {
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
