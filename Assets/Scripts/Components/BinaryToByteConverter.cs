using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryToByteConverter : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // 获取每个输入引脚的值，并拼接成一个8位二进制数
        string binaryValue = "";
        for (int i = 0; i < 8; i++)
        {
            int inputValue = InputPins.GetValue(i);  // 获取每个引脚的值（0 或 1）
            binaryValue += inputValue.ToString();    // 拼接成字符串
        }

        // 将二进制字符串转换为 byte
        byte byteValue = ConvertBinaryToByte(binaryValue);

        // 输出转换后的 byte
        OutputPins.SetValue(0, byteValue);
    }

    protected override void InitShape()
    {
        // 输入引脚的顺序从高位到低位，分别为：9, 7, 5, 3, 0, -2, -4, -6
        int[] inputPositionsX = { -6, -4, -2, 0, 3, 5, 7, 9 };

        for (int i = 0; i < 8; i++)
        {
            int xPosition = inputPositionsX[i];  // 获取相应的 x 坐标
            InputPins.AddPin(i, Type.BIT, xPosition, 2, false);  // 添加输入引脚
        }

        // 添加一个输出引脚，位置为 (10, 0)
        OutputPins.AddPin(0, Type.BYTE, 10, 0);

        // 添加形状位置
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
    }

    // 将二进制字符串转换为 byte
    byte ConvertBinaryToByte(string binary)
    {
        return Convert.ToByte(binary, 2);
    }
}
