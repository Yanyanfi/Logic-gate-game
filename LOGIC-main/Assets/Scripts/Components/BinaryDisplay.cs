using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // 引入 TextMeshPro 命名空间

public class BinaryDisplay : NewComponent
{
    public TextMeshProUGUI resultText;  // 用于显示结果的 UI 文本（使用 TextMeshPro）

    public override void HandleInputs(object sender, EventArgs e)
    {
        // 获取输入的 byte 值
        byte byteValue = (byte)InputPins.GetValue(0);

        // 将 byte 转换为 8 位二进制字符串
        string binaryValue = Convert.ToString(byteValue, 2).PadLeft(8, '0');

        // 输出结果为二进制字符串
        resultText.text = binaryValue;
    }

    protected override void InitShape()
    {
        // 添加一个输入引脚
        InputPins.AddPin(0, ValueType.BYTE, -2, 0, false);

        // 添加形状位置
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
    }
}
