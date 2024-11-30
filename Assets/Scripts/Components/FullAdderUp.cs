/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAdderUp : NewComponent
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

        // 计算零标志（Zero Flag）
        int zeroFlag = (sum == 0) ? 1 : 0;

        // 计算符号标志（Sign Flag） - 通过查看 sum 的符号位
        int signFlag = (sum < 0) ? 1 : 0;

        // 计算溢出标志（Overflow Flag）
        // 溢出条件：A 和 B 符号相同，但 Sum 的符号不同
        int overflowFlag = ((inputA ^ inputB) & (inputB ^ sum)) >> 31;  // (A XOR B) AND (B XOR Sum) 检查是否溢出

        // 设置输出信号
        OutputPins.SetValue(0, sum);           // 输出 Sum
        OutputPins.SetValue(1, carryOut);      // 输出 CarryOut
        OutputPins.SetValue(2, zeroFlag);      // 输出 Zero Flag
        OutputPins.SetValue(3, overflowFlag);  // 输出 Overflow Flag
        OutputPins.SetValue(4, signFlag);      // 输出 Sign Flag
    }

    protected override void InitShape()
    {
        // 添加输入引脚
        InputPins.AddPin(0, Type.BIT, -2, 2, false);  // 输入 A
        InputPins.AddPin(1, Type.BIT, -2, 0, false);  // 输入 B
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
        OutputPins.AddPin(0, Type.BIT, 2, 2);   // 输出 Sum
        OutputPins.AddPin(1, Type.BIT, 2, 1);  // 输出 CarryOut
        OutputPins.AddPin(2, Type.BIT, 2, 0);   // 输出 Zero Flag
        OutputPins.AddPin(3, Type.BIT, 2, -1);  // 输出 Overflow Flag
        OutputPins.AddPin(4, Type.BIT, 2, -2);   // 输出 Sign Flag
    }
}*/

//输入均为1位时用以下代码！

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAdderUp : NewComponent
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

        // 计算零标志（Zero Flag）
        int zeroFlag = (sum == 0) ? 1 : 0;

        // 计算符号标志（Sign Flag） - 对于1位加法器，符号标志与sum相同
        int signFlag = sum;  // Sum == 1 -> positive (sign = 1), Sum == 0 -> negative (sign = 0)

        // 计算溢出标志（Overflow Flag）
        // 溢出条件：A 和 B 符号相同，但 Sum 的符号不同
        int overflowFlag = ((inputA == inputB) && (sum != (inputA ^ inputB))) ? 1 : 0;  // 如果 A 和 B 相同，但 Sum 与它们的和不同，则溢出

        // 设置输出信号
        OutputPins.SetValue(0, sum);           // 输出 Sum
        OutputPins.SetValue(1, carryOut);      // 输出 CarryOut
        OutputPins.SetValue(2, zeroFlag);      // 输出 Zero Flag
        OutputPins.SetValue(3, overflowFlag);  // 输出 Overflow Flag
        OutputPins.SetValue(4, signFlag);      // 输出 Sign Flag
    }

    protected override void InitShape()
    {
        // 添加输入引脚
        InputPins.AddPin(0, Type.BIT, -2, 2, false);  // 输入 A
        InputPins.AddPin(1, Type.BIT, -2, 0, false);  // 输入 B
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
        OutputPins.AddPin(0, Type.BIT, 2, 2);   // 输出 Sum
        OutputPins.AddPin(1, Type.BIT, 2, 1);   // 输出 CarryOut
        OutputPins.AddPin(2, Type.BIT, 2, 0);   // 输出 Zero Flag
        OutputPins.AddPin(3, Type.BIT, 2, -1);  // 输出 Overflow Flag
        OutputPins.AddPin(4, Type.BIT, 2, -2);  // 输出 Sign Flag
    }
}
