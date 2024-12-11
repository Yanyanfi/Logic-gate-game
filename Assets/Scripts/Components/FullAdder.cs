using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAdder : NewComponent
{
    public override void HandleInputs(object sender, EventArgs e)
    {
        // ��ȡ�����ź�
        int inputA = InputPins.GetValue(0);    // ���� A
        int inputB = InputPins.GetValue(1);    // ���� B
        int carryIn = InputPins.GetValue(2);   // ���� CarryIn

        // ���� Sum �� CarryOut
        //int sum = inputA ^ inputB ^ carryIn;  // Sum = A XOR B XOR CarryIn
        //int carryOut = (inputA & inputB) | (carryIn & (inputA ^ inputB));  // CarryOut = (A AND B) OR (CarryIn AND (A XOR B))
        // ����8λ�ӷ��Ľ��
        // ����8λ�ӷ��Ľ��
        int sum = 0;
        int carryOut = 0;  // ���ս�λ���

        int currentCarry = carryIn;  // ��ʼ��λΪ����� CarryIn

        // �����λ�����λ���мӷ�����
        for (int i = 0; i < 8; i++)
        {
            // ��ȡÿһλ
            int bitA = (inputA >> i) & 1;  // ��ȡ�� i λ
            int bitB = (inputB >> i) & 1;  // ��ȡ�� i λ
            int bitCarryIn = (currentCarry >> i) & 1;  // ��ȡ��ǰλ�Ľ�λ����

            // ���㵱ǰλ�� sum
            int bitSum = bitA ^ bitB ^ bitCarryIn;  // XOR ���� sum

            // ����ǰλ�� sum ��ϳ����յ� sum
            sum |= (bitSum << i);  // ����ǰλ�� sum �ŵ����ʵ�λ��

            // ���㵱ǰλ�Ľ�λ������н�λ�����󴫵ݣ�
            int bitCarryOut = (bitA & bitB) | (bitCarryIn & (bitA ^ bitB));  // AND �� OR ���� carryOut

            // �������յĽ�λ���
            // ������һλ�ļ����н�λ��carryOut Ϊ 1������Ϊ 0
            if (i == 7 && bitCarryOut == 1)
            {
                carryOut = 1;  // �������λ�н�λ������ carryOut Ϊ 1
            }

            // ���½�λ���ݵ���һλ
            currentCarry = (bitCarryOut << (i + 1));  // �� carryOut �����ƶ������ݵ���һλ
        }
        // ��������ź�
        OutputPins.SetValue(0, sum);    // ��� Sum
        OutputPins.SetValue(1, carryOut); // ��� CarryOut
    }

    protected override void InitShape()
    {
        // �����������
        InputPins.AddPin(0, ValueType.BIT, -2, 2, false); // ���� A
        InputPins.AddPin(1, ValueType.BIT, -2, 0, false); // ���� B
        InputPins.AddPin(2, ValueType.BIT, -2, -2, false); // ���� CarryIn

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
        OutputPins.AddPin(1, ValueType.BIT, 2, -1); // ��� CarryOut
    }
}