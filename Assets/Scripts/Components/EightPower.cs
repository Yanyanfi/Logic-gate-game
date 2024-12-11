using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightPower : NewComponent
{
    protected override void InitShape()
    {
        // �������ŵĹ̶�λ�ã����ϵ�������
        int[] posY = { 24, 21, 18, 15, 11, 8, 5, 2 };
        int posX = 3; // �������ŵ� X ����Ϊ 3

        // ��̬��Ӱ˸��������
        for (int i = 0; i < posY.Length; i++)
        {
            OutputPins.AddPin(i, ValueType.BIT, posX, posY[i]);
        }

        // ������״
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
    }
}
