using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryToByteConverter : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // ��ȡÿ���������ŵ�ֵ����ƴ�ӳ�һ��8λ��������
        string binaryValue = "";
        for (int i = 0; i < 8; i++)
        {
            int inputValue = InputPins.GetValue(i);  // ��ȡÿ�����ŵ�ֵ��0 �� 1��
            binaryValue += inputValue.ToString();    // ƴ�ӳ��ַ���
        }

        // ���������ַ���ת��Ϊ byte
        byte byteValue = ConvertBinaryToByte(binaryValue);

        // ���ת����� byte
        OutputPins.SetValue(0, byteValue);
    }

    protected override void InitShape()
    {
        // �������ŵ�˳��Ӹ�λ����λ���ֱ�Ϊ��9, 7, 5, 3, 0, -2, -4, -6
        int[] inputPositionsX = { -6, -4, -2, 0, 3, 5, 7, 9 };

        for (int i = 0; i < 8; i++)
        {
            int xPosition = inputPositionsX[i];  // ��ȡ��Ӧ�� x ����
            InputPins.AddPin(i, ValueType.BIT, xPosition, 2, false);  // �����������
        }

        // ���һ��������ţ�λ��Ϊ (10, 0)
        OutputPins.AddPin(0, ValueType.BYTE, 10, 0);

        // �����״λ��
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
    }

    // ���������ַ���ת��Ϊ byte
    byte ConvertBinaryToByte(string binary)
    {
        return Convert.ToByte(binary, 2);
    }
}
