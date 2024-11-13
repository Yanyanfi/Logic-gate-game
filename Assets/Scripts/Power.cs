using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Power : Component
{
    public override void SetPosition(Vector2Int pos)
    {
        PositionOfBody.Clear();
        PositionOfOutputPin.Clear();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
                PositionOfBody.Add(new Vector2Int(pos.x + i, pos.y + j));
        }
        PositionOfOutputPin.Add(new Vector2Int(pos.x + 2, pos.y));
    }
    protected override void ExecuteLogic()
    {
        base.ExecuteLogic();
    }
    private void Awake()
    {
        InitComponent();
        outputValue = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
                RelativePositionOfBody.Add(new Vector2Int(i, j));
        }
        RelativePositionOfOutputPin.Add(new Vector2Int(2, 0));
    }
}
