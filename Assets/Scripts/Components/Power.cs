using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Power : NewComponent
{
    protected override void InitShape()
    {
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
        OutputPins.AddPin(0, ValueType.BIT, 3, 0);
    }
}
