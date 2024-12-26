using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FixedOutputHigh : NewComponent
{
    protected override void InitShape()
    {
        // 初始化组件的形状
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }

        // 固定引脚输出为1
        OutputPins.AddPin(0, ValueType.BIT, 0, 2);
        SetPinValue(0, 1); // 固定值设置为1
    }

    // 添加方法以设置引脚值
    private void SetPinValue(int pinIndex, int value)
    {
        if (OutputPins != null && OutputPins.Count > pinIndex)
        {
            OutputPins[pinIndex].Value = value;
        }
    }
}
