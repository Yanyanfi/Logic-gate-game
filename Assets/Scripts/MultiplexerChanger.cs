using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplexerChanger : MonoBehaviour
{
    private NewComponent component;

    // ����ѡ���źţ�0��1��2
    private int selectSignal = 0; // ��ʼѡ���ź�Ϊ0 (��ʾ״̬ 0)

    // ����ѡ���źű仯��0 -> 1 -> 2 -> 0 ...��
    private void ChangeSelectSignal()
    {
        selectSignal = (selectSignal + 1) % 3; // 0, 1, 2 ѭ��
    }

    // ����������ŵ�ֵ������ѡ���źŵ�ֵ
    private void ChangeValue()
    {
        // ��ȡ��һ��������ţ�����ֻ��һ��������ţ�
        var outputPin = component.OutputPins[0]; // ʹ�� GetPin ����ȡ����

        // ����ѡ���ź��޸�������ŵ�ֵ
        // ѡ���ź�Ϊ 0���������Ϊ 0
        // ѡ���ź�Ϊ 1���������Ϊ 1
        // ѡ���ź�Ϊ 2���������Ϊ 2
        outputPin.Value = selectSignal;
    }

    // �����ָ����ͣ������ϣ������� "E" ��ʱ����
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeSelectSignal(); // �л�ѡ���ź�
            ChangeValue(); // ����������ŵ�ֵ
        }
    }

    private void Awake()
    {
        component = GetComponent<NewComponent>();
        if (component == null)
        {
            Destroy(this); // ���û���ҵ� NewComponent�����ٸýű�
        }
    }
}
