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
        //int sum = inputA ^ inputB ^ carryIn;  // Sum = A XOR B XOR CarryIn
        //int carryOut = (inputA & inputB) | (carryIn & (inputA ^ inputB));  // CarryOut = (A AND B) OR (CarryIn AND (A XOR B))
        // 计算8位加法的结果
        // 计算8位加法的结果
        int sum = 0;
        int carryOut = 0;  // 最终进位输出

        int currentCarry = carryIn;  // 初始进位为输入的 CarryIn

        // 从最低位到最高位进行加法运算
        for (int i = 0; i < 8; i++)
        {
            // 获取每一位
            int bitA = (inputA >> i) & 1;  // 获取第 i 位
            int bitB = (inputB >> i) & 1;  // 获取第 i 位
            int bitCarryIn = (currentCarry >> i) & 1;  // 获取当前位的进位输入

            // 计算当前位的 sum
            int bitSum = bitA ^ bitB ^ bitCarryIn;  // XOR 计算 sum

            // 将当前位的 sum 组合成最终的 sum
            sum |= (bitSum << i);  // 将当前位的 sum 放到合适的位置

            // 计算当前位的进位（如果有进位则向左传递）
            int bitCarryOut = (bitA & bitB) | (bitCarryIn & (bitA ^ bitB));  // AND 和 OR 计算 carryOut

            // 计算最终的进位输出
            // 如果最后一位的计算有进位，carryOut 为 1，否则为 0
            if (i == 7 && bitCarryOut == 1)
            {
                carryOut = 1;  // 如果最终位有进位，设置 carryOut 为 1
            }

            // 更新进位传递到下一位
            currentCarry = (bitCarryOut << (i + 1));  // 将 carryOut 向左移动，传递到下一位
        }
        // 设置输出信号
        OutputPins.SetValue(0, sum);    // 输出 Sum
        OutputPins.SetValue(1, carryOut); // 输出 CarryOut
    }

    protected override void InitShape()
    {
        // 添加输入引脚
        InputPins.AddPin(0, ValueType.BIT, -2, 2, false); // 输入 A
        InputPins.AddPin(1, ValueType.BIT, -2, 0, false); // 输入 B
        InputPins.AddPin(2, ValueType.BIT, -2, -2, false); // 输入 CarryIn

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
        OutputPins.AddPin(1, ValueType.BIT, 2, -1); // 输出 CarryOut
    }
}