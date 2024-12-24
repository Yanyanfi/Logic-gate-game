using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfAdder : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // 获取输入信号
        int inputA = InputPins.GetValue(0);
        int inputB = InputPins.GetValue(1);

        // 计算 Sum 和 Carry
        int sum = inputA ^ inputB; // XOR 操作
        int carry = inputA & inputB; // AND 操作

        // 设置输出信号
        OutputPins.SetValue(0, sum);  // 输出 Sum
        OutputPins.SetValue(1, carry); // 输出 Carry
    }

    protected override void InitShape()
    {
        // 添加输入引脚
        InputPins.AddPin(0, ValueType.BIT, -2, 1, false); // 输入 A
        InputPins.AddPin(1, ValueType.BIT, -2, -1, false); // 输入 B

        // 配置形状
        Debug.LogFormat("Body.AddRelativePosition start");
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }

        // 添加输出引脚
        OutputPins.AddPin(0, ValueType.BIT, 2, 1); // 输出 Sum
        OutputPins.AddPin(1, ValueType.BIT, 2, -1); // 输出 Carry
    }
}
