using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // ���� TextMeshPro �����ռ�

public class BinaryDisplay : NewComponent
{
    public TextMeshProUGUI resultText;  // ������ʾ����� UI �ı���ʹ�� TextMeshPro��

    public override void HandleInputs(object sender, EventArgs e)
    {
        // ��ȡ����� byte ֵ
        byte byteValue = (byte)InputPins.GetValue(0);

        // �� byte ת��Ϊ 8 λ�������ַ���
        string binaryValue = Convert.ToString(byteValue, 2).PadLeft(8, '0');

        // ������Ϊ�������ַ���
        resultText.text = binaryValue;
    }

    protected override void InitShape()
    {
        // ���һ����������
        InputPins.AddPin(0, ValueType.BYTE, -2, 0, false);

        // �����״λ��
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
    }
}
