using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTGate : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        int input = InputPins.GetValue(0);  // 获取输入引脚的值
        // 非门逻辑：输入为1，输出为0；输入为0，输出为1
        if (input == 1)
            OutputPins.SetValue(0, 0);  // 输入1，输出0
        else
            OutputPins.SetValue(0, 1);  // 输入0，输出1
    }

    protected override void InitShape()
    {
        InputPins.AddPin(0, ValueType.BIT, -2, 0, false);  // 添加一个输入引脚
        Debug.LogFormat("Body.AddRelativePosition start");
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);  // 添加Body的相对位置
            }
        }
        OutputPins.AddPin(0, ValueType.BIT, 3, 0);  // 添加一个输出引脚
    }
}
