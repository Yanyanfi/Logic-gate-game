using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class NANDGate :LogicGate
{
    protected override void ExecuteLogic()
    {
        if (inputWires.Count >= 2)
        {
            bool result = false;
            foreach(var wire in inputWires)
            {
                if (wire.Value == false)
                    result = true;
            }
            SetOutputValue(result); // 将结果传递到输出线路
        }
        else
        {
            SetOutputValue(false); 
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
        RelativePositionOfInputPin=new();
        RelativePositionOfBody = new();
        RelativePositionOfOutputPin = new();
        PositionOfBody = new();
        PositionOfInputPin = new();
        PositionOfOutputPin = new();
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
