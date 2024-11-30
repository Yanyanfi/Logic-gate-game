using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoMultiplexer : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // 获取输入 A、B 和选择信号 S
        int inputA = InputPins.GetValue(0); // 输入 A
        int inputB = InputPins.GetValue(1); // 输入 B
        int selectSignal = InputPins.GetValue(2); // 选择信号 S

        // 根据选择信号 S 来决定输出
        if (selectSignal == 0)
        {
            OutputPins.SetValue(0, inputA); // S == 0 时，输出 A
        }
        else
        {
            OutputPins.SetValue(0, inputB); // S == 1 时，输出 B
        }
    }

    protected override void InitShape()
    {
        // 添加输入引脚 A、B 和选择信号 S
        InputPins.AddPin(0, Type.BIT, -2, 1, false); // 输入 A
        InputPins.AddPin(1, Type.BIT, -2, -1, false); // 输入 B
        InputPins.AddPin(2, Type.BIT, 0, 3, false); // 选择信号 S

        // 设置该元件的形状
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }

        // 添加输出引脚
        OutputPins.AddPin(0, Type.BIT, 2, 0); // 输出引脚
    }
}
