using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class XNORGate : NewComponent
{   //处理输入引脚的变化
    public override void HandleInputs(object sender, EventArgs e)
    {
        int inputA = InputPins.GetValue(0);
        int inputB = InputPins.GetValue(1);
        //两个输入值相同，则值为1；不同，则值为0
        if (inputA == inputB)
            OutputPins.SetValue(0, 1);
        else
            OutputPins.SetValue(0, 0);
    }
    //初始化XNOR门的形状和引脚
    protected override void InitShape()
    {
        //添加输入引脚
        InputPins.AddPin(0, ValueType.BIT, -2, 1, false);
        InputPins.AddPin(1, ValueType.BIT, -2, -1, false);

        Debug.LogFormat("Body.AddRelativePosition start");
        //XNOR主体：3x3的方块
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
        //添加输出引脚
        OutputPins.AddPin(0, ValueType.BIT, 2, 0);
    }
}