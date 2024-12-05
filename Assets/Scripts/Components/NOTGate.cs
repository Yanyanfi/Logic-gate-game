using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTGate : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        int input = InputPins.GetValue(0);  // ��ȡ�������ŵ�ֵ
        // �����߼�������Ϊ1�����Ϊ0������Ϊ0�����Ϊ1
        if (input == 1)
            OutputPins.SetValue(0, 0);  // ����1�����0
        else
            OutputPins.SetValue(0, 1);  // ����0�����1
    }

    protected override void InitShape()
    {
        InputPins.AddPin(0, ValueType.BIT, -2, 0, false);  // ���һ����������
        Debug.LogFormat("Body.AddRelativePosition start");
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);  // ���Body�����λ��
            }
        }
        OutputPins.AddPin(0, ValueType.BIT, 3, 0);  // ���һ���������
    }
}
