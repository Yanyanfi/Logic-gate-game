using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryPadding : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // ��ȡ����Ķ��������֣�������һ��С��8λ��������
        int inputValue = InputPins.GetValue(0);  // ��ȡ���������ֵ

        // ȷ������ֵС�� 256��8 λ��
        if (inputValue < 0 || inputValue > 255)
        {
            Debug.LogError("Input value should be in the range of 0 to 255.");
            return;
        }

        // ͨ��ת���ɶ������ַ�������չΪ 8 λ
        byte paddedValue = (byte)(inputValue & 0xFF);  // ȷ��Ϊ 8 λ

        // �����������ֵΪ��չ��� 8 λֵ
        OutputPins.SetValue(0, paddedValue);  // �����չ��Ķ�����ֵ
    }

    protected override void InitShape()
    {
        // ����������ţ�����Ϊ BYTE����������ֵ��
        InputPins.AddPin(0, ValueType.BYTE, -2, 0, false);
        // ������״λ��
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
        // ���������ţ�����Ϊ BYTE�������չ��Ķ�����ֵ��
        OutputPins.AddPin(0, ValueType.BYTE, 2, 0);
    }
}
