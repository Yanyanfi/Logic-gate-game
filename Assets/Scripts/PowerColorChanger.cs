using Assets.Scripts.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Power power;
    private void ChangeColor(object sender,EventArgs e)
    {
        int value = power.OutputPins.GetValue(0);
        switch (value)
        {
            case 0:
                spriteRenderer.color = Color.white;
                break;
            case 1:
                spriteRenderer.color = Color.green;
                break;
            default:
                break;
        }
    }
    void Awake()
    {
        power = transform.GetComponentInParent<Power>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        power.OutputPins.SubscribeToPins(ChangeColor);
    }
}
