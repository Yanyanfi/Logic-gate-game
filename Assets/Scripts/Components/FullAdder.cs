using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAdder : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // 获取输入信号
        int inputA = InputPins.GetValue(0);    // 输入 A
        int inputB = InputPins.GetValue(1);    // 输入 B
        int carryIn = InputPins.GetValue(2);   // 输入 CarryIn

        // 计算 Sum 和 CarryOut
        int sum = inputA ^ inputB ^ carryIn;  // Sum = A XOR B XOR CarryIn
        int carryOut = (inputA & inputB) | (carryIn & (inputA ^ inputB));  // CarryOut = (A AND B) OR (CarryIn AND (A XOR B))

        // 设置输出信号
        OutputPins.SetValue(0, sum);    // 输出 Sum
        OutputPins.SetValue(1, carryOut); // 输出 CarryOut
    }

    protected override void InitShape()
    {
        // 添加输入引脚
        InputPins.AddPin(0, Type.BIT, -2, 2, false); // 输入 A
        InputPins.AddPin(1, Type.BIT, -2, 0, false); // 输入 B
        InputPins.AddPin(2, Type.BIT, -2, -2, false); // 输入 CarryIn

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
        OutputPins.AddPin(0, Type.BIT, 2, 1); // 输出 Sum
        OutputPins.AddPin(1, Type.BIT, 2, -1); // 输出 CarryOut
    }
}
