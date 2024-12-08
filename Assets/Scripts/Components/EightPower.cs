using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightPower : NewComponent
{
    protected override void InitShape()
    {
        // 定义引脚的固定位置，从上到下排列
        int[] posY = { 24, 21, 18, 15, 11, 8, 5, 2 };
        int posX = 3; // 所有引脚的 X 坐标为 3

        // 动态添加八个输出引脚
        for (int i = 0; i < posY.Length; i++)
        {
            OutputPins.AddPin(i, Type.BIT, posX, posY[i]);
        }

        // 配置形状
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
    }
}
