using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeMultiplexer : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // ��ȡ���� A��B��C ��ѡ���ź� S
        int inputA = InputPins.GetValue(0); // ���� A
        int inputB = InputPins.GetValue(1); // ���� B
        int inputC = InputPins.GetValue(2); // ���� C
        int selectSignal = InputPins.GetValue(3); // ѡ���ź� S

        // ����ѡ���ź� S ���������
        if (selectSignal == 0) // 00
        {
            OutputPins.SetValue(0, inputA); // S == 00 ʱ����� A
        }
        else if (selectSignal == 1) // 01
        {
            OutputPins.SetValue(0, inputB); // S == 01 ʱ����� B
        }
        else // S == 10 �� S == 11
        {
            OutputPins.SetValue(0, inputC); // S == 10 �� 11 ʱ����� C
        }
    }

    protected override void InitShape()
    {
        // ����������� A��B��C ��ѡ���ź� S
        InputPins.AddPin(0, ValueType.BIT, -2, 2, false); // ���� A
        InputPins.AddPin(1, ValueType.BIT, -2, 0, false); // ���� B
        InputPins.AddPin(2, ValueType.BIT, -2, -2, false); // ���� C
        InputPins.AddPin(3, ValueType.BIT, 0, 4, false); // ѡ���ź� S

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
