using System;
using UnityEngine;

public class LightBulb : NewComponent
{
    private SpriteRenderer spriteRenderer;  // ���ڿ���Բ�ε���ɫ
    public Sprite lightOnSprite;  // ��������״̬ͼƬ
    public Sprite lightOffSprite; // ���ݰ���״̬ͼƬ

    // ʵ�ֳ��󷽷� InitShape
    protected override void InitShape()
    {
        // ��ȡ SpriteRenderer ���
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ϊ��������Ĭ�ϵ�ͼƬ�������ʼ״̬ΪϨ��״̬��
        spriteRenderer.sprite = lightOffSprite;

        // ����������ţ�λ��Ϊ (-2, 0)
        InputPins.AddPin(0, ValueType.BIT, -2, 0, false);

        // ����Բ�ε�λ�ã���������һ�� 2D Բ��
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Body.AddRelativePosition(x, y);
            }
        }
    }

    // ��д HandleInputs ����
    public override void HandleInputs(object sender, EventArgs e)
    {
        // ��ȡ�������ŵ��ź�ֵ
        int inputSignal = InputPins.GetValue(0);

        // ���������źŵ�ֵ���л� Sprite
        if (inputSignal == 1)
        {
            spriteRenderer.sprite = lightOnSprite;  // �ź�Ϊ1����ʾ���ĵ���ͼƬ
        }
        else
        {
            spriteRenderer.sprite = lightOffSprite; // �ź�Ϊ0����ʾ���ĵ���ͼƬ
        }
    }
}
