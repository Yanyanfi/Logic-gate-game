using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryPadding : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // 获取输入的二进制数字（假设是一个小于8位的整数）
        int inputValue = InputPins.GetValue(0);  // 获取输入的整数值

        // 确保输入值小于 256（8 位）
        if (inputValue < 0 || inputValue > 255)
        {
            Debug.LogError("Input value should be in the range of 0 to 255.");
            return;
        }

        // 通过转换成二进制字符串并扩展为 8 位
        byte paddedValue = (byte)(inputValue & 0xFF);  // 确保为 8 位

        // 设置输出引脚值为扩展后的 8 位值
        OutputPins.SetValue(0, paddedValue);  // 输出扩展后的二进制值
    }

    protected override void InitShape()
    {
        // 添加输入引脚，类型为 BYTE（接受整数值）
        InputPins.AddPin(0, Type.BYTE, -2, 0, false);
        // 配置形状位置
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
        // 添加输出引脚，类型为 BYTE（输出扩展后的二进制值）
        OutputPins.AddPin(0, Type.BYTE, 2, 0);
    }
}
