using System;
using UnityEngine;

public class LightBulb : NewComponent
{
    private SpriteRenderer spriteRenderer;  // 用于控制圆形的颜色
    public Sprite lightOnSprite;  // 灯泡亮的状态图片
    public Sprite lightOffSprite; // 灯泡暗的状态图片

    // 实现抽象方法 InitShape
    protected override void InitShape()
    {
        // 获取 SpriteRenderer 组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 为灯泡设置默认的图片（假设初始状态为熄灭状态）
        spriteRenderer.sprite = lightOffSprite;

        // 添加输入引脚，位置为 (-2, 0)
        InputPins.AddPin(0, Type.BIT, -2, 0, false);

        // 设置圆形的位置，假设它是一个 2D 圆形
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
    }

    // 重写 HandleInputs 方法
    public override void HandleInputs(object sender, EventArgs e)
    {
        // 获取输入引脚的信号值
        int inputSignal = InputPins.GetValue(0);

        // 根据输入信号的值来切换 Sprite
        if (inputSignal == 1)
        {
            spriteRenderer.sprite = lightOnSprite;  // 信号为1，显示亮的灯泡图片
        }
        else
        {
            spriteRenderer.sprite = lightOffSprite; // 信号为0，显示暗的灯泡图片
        }
    }
}
