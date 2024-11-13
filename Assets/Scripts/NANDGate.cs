using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NANDGate :Component
{
    protected override void ExecuteLogic()
    {
        if (inputWires.Count >= 2)
        {
            int result = 0;
            foreach(var wire in inputWires)
            {
                if (wire.Value == 0)
                    result = 1;
            }
            OutputValue = result;
        }
        else
        {
            OutputValue = 1;
        }
    }
    public override void SetPosition(Vector2Int pos)
    {
        PositionOfInputPin.Clear();
        PositionOfBody.Clear();
        PositionOfOutputPin.Clear();
        for (int i = -1; i <= 1; i++)
        {
            for(int j=-1;j<=1;j++)
                PositionOfBody.Add(new Vector2Int(pos.x+i,pos.y+j));
        }
        PositionOfInputPin.Add(new Vector2Int(pos.x - 2, pos.y + 1));
        PositionOfInputPin.Add(new Vector2Int(pos.x - 2, pos.y - 1));
        PositionOfOutputPin.Add(new Vector2Int(pos.x + 2, pos.y));
    }

    private void Awake()
    {
        InitComponent();
        OutputValue = 1;   
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
                RelativePositionOfBody.Add(new Vector2Int( i,j));
        }
        RelativePositionOfInputPin.Add(new Vector2Int(- 2, 1));
        RelativePositionOfInputPin.Add(new Vector2Int( -2, -1));
        RelativePositionOfOutputPin.Add(new Vector2Int(2, 0));
    }

    
}
