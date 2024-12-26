using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FixedOutputHigh : NewComponent
{
    protected override void InitShape()
    {
        // ��ʼ���������״
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }

        // �̶��������Ϊ1
        OutputPins.AddPin(0, ValueType.BIT, 0, 2);
        SetPinValue(0, 1); // �̶�ֵ����Ϊ1
    }

    // ��ӷ�������������ֵ
    private void SetPinValue(int pinIndex, int value)
    {
        if (OutputPins != null && OutputPins.Count > pinIndex)
        {
            OutputPins[pinIndex].Value = value;
        }
    }
}
