using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeMultiplexer : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // 获取输入 A、B、C 和选择信号 S
        int inputA = InputPins.GetValue(0); // 输入 A
        int inputB = InputPins.GetValue(1); // 输入 B
        int inputC = InputPins.GetValue(2); // 输入 C
        int selectSignal = InputPins.GetValue(3); // 选择信号 S

        // 根据选择信号 S 来决定输出
        if (selectSignal == 0) // 00
        {
            OutputPins.SetValue(0, inputA); // S == 00 时，输出 A
        }
        else if (selectSignal == 1) // 01
        {
            OutputPins.SetValue(0, inputB); // S == 01 时，输出 B
        }
        else // S == 10 或 S == 11
        {
            OutputPins.SetValue(0, inputC); // S == 10 或 11 时，输出 C
        }
    }

    protected override void InitShape()
    {
        // 添加输入引脚 A、B、C 和选择信号 S
        InputPins.AddPin(0, ValueType.BIT, -2, 2, false); // 输入 A
        InputPins.AddPin(1, ValueType.BIT, -2, 0, false); // 输入 B
        InputPins.AddPin(2, ValueType.BIT, -2, -2, false); // 输入 C
        InputPins.AddPin(3, ValueType.BIT, 0, 4, false); // 选择信号 S

        // 设置该元件的形状
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }

        // 添加输出引脚
        OutputPins.AddPin(0, ValueType.BIT, 2, 0); // 输出引脚
    }
}
