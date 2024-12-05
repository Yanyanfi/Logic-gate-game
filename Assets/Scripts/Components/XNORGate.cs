using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class XNORGate : NewComponent
{   //�����������ŵı仯
    public override void HandleInputs(object sender, EventArgs e)
    {
        int inputA = InputPins.GetValue(0);
        int inputB = InputPins.GetValue(1);
        //��������ֵ��ͬ����ֵΪ1����ͬ����ֵΪ0
        if (inputA == inputB)
            OutputPins.SetValue(0, 1);
        else
            OutputPins.SetValue(0, 0);
    }
    //��ʼ��XNOR�ŵ���״������
    protected override void InitShape()
    {
        //�����������
        InputPins.AddPin(0, ValueType.BIT, -2, 1, false);
        InputPins.AddPin(1, ValueType.BIT, -2, -1, false);

        Debug.LogFormat("Body.AddRelativePosition start");
        //XNOR���壺3x3�ķ���
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
        //����������
        OutputPins.AddPin(0, ValueType.BIT, 2, 0);
    }
}