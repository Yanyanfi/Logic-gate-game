using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoMultiplexer : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // ��ȡ���� A��B ��ѡ���ź� S
        int inputA = InputPins.GetValue(0); // ���� A
        int inputB = InputPins.GetValue(1); // ���� B
        int selectSignal = InputPins.GetValue(2); // ѡ���ź� S

        // ����ѡ���ź� S ���������
        if (selectSignal == 0)
        {
            OutputPins.SetValue(0, inputA); // S == 0 ʱ����� A
        }
        else
        {
            OutputPins.SetValue(0, inputB); // S == 1 ʱ����� B
        }
    }

    protected override void InitShape()
    {
        // ����������� A��B ��ѡ���ź� S
        InputPins.AddPin(0, ValueType.BIT, -2, 1, false); // ���� A
        InputPins.AddPin(1, ValueType.BIT, -2, -1, false); // ���� B
        InputPins.AddPin(2, ValueType.BIT, 0, 3, false); // ѡ���ź� S

        // ���ø�Ԫ������״
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }

        // ����������
        OutputPins.AddPin(0, ValueType.BIT, 2, 0); // �������
    }
}
