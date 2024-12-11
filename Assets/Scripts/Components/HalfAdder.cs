using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfAdder : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // ��ȡ�����ź�
        int inputA = InputPins.GetValue(0);
        int inputB = InputPins.GetValue(1);

        // ���� Sum �� Carry
        int sum = inputA ^ inputB; // XOR ����
        int carry = inputA & inputB; // AND ����

        // ��������ź�
        OutputPins.SetValue(0, sum);  // ��� Sum
        OutputPins.SetValue(1, carry); // ��� Carry
    }

    protected override void InitShape()
    {
        // �����������
        InputPins.AddPin(0, ValueType.BIT, -2, 1, false); // ���� A
        InputPins.AddPin(1, ValueType.BIT, -2, -1, false); // ���� B

        // ������״
        Debug.LogFormat("Body.AddRelativePosition start");
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }

        // ����������
        OutputPins.AddPin(0, ValueType.BIT, 2, 1); // ��� Sum
        OutputPins.AddPin(1, ValueType.BIT, 2, -1); // ��� Carry
    }
}
