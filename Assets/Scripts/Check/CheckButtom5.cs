using UnityEngine;
using UnityEngine.UI; // ���� UI ����ʹ�ð�ť
using System.Collections;
using System.Collections.Generic;

public class CheckButtom5 : MonoBehaviour
{
    bool allPassed = false ; // ���ڱ���Ƿ����а�����ͨ��

    [SerializeField] private NewComponent Power1; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power2; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power3; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power4; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power5; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power6; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power7; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power8; // �����еĵ�Դ���

    [SerializeField] private LightBulb LightBulb1; // �����еĵ������
    [SerializeField] private LightBulb LightBulb2; // �����еĵ������
    [SerializeField] private LightBulb LightBulb3; // �����еĵ������

    [SerializeField] private Button CheckButton;  // �����еİ�ť����
    [SerializeField] private ResultCircle[] ResultCircle1;  // �����еİ�ť����
    [SerializeField] private ResultCircle[] ResultCircle2;  // �����еİ�ť����
    [SerializeField] private ResultCircle[] ResultCircle3;  // �����еİ�ť����

    public void CCStart()
    {
        if (CheckButton != null)
        {
            // Ϊ��ť�󶨵���¼�
            CheckButton.onClick.AddListener(CheckAndSetPower);
        }
        else
        {
            Debug.LogWarning("��ťδ��ȷ���ã�");
        }
    }

    private void CheckAndSetPower()
    {
        if (Power1 == null || Power2 == null || Power3 == null || Power4 == null ||
            Power5 == null || Power6 == null || Power7 == null || Power8 == null ||
            LightBulb1 == null || LightBulb2 == null || LightBulb3 == null)
        {
            Debug.LogWarning("��Դ��������δ��ȷ���ã�");
            return;
        }

        // ����ֵ��ǰ��λ���� X������λ���� Y
        int[,] inputPowerValues = {
            { 0, 0, 0, 0, 0, 0, 0, 0 },  // ��һ���������
            { 0, 1, 1, 1, 1, 0, 0, 0 },  // �ڶ����������
            { 1, 0, 0, 0, 0, 1, 1, 1 }   // �������������
        };

        // ���ݵ��������ֵ
        int[,] bulbExpectedValues = {
            { 1, 0, 0 },  // ��һ��������ݵ���״̬
            { 0, 1, 1 },  // �ڶ���������ݵ���״̬
            { 0, 0, 1 }   // ������������ݵ���״̬
        };
        int tag = 0;
        for (int i = 0; i < inputPowerValues.GetLength(0); i++)
        {
            // Ϊ��Դ������õ�ǰ�Ĳ�������ֵ
            Power1.OutputPins.SetValue(0, inputPowerValues[i, 0]);
            Power2.OutputPins.SetValue(0, inputPowerValues[i, 1]);
            Power3.OutputPins.SetValue(0, inputPowerValues[i, 2]);
            Power4.OutputPins.SetValue(0, inputPowerValues[i, 3]);

            Power5.OutputPins.SetValue(0, inputPowerValues[i, 4]);
            Power6.OutputPins.SetValue(0, inputPowerValues[i, 5]);
            Power7.OutputPins.SetValue(0, inputPowerValues[i, 6]);
            Power8.OutputPins.SetValue(0, inputPowerValues[i, 7]);

            // ��ȡ���ݵ�ʵ�����ֵ
            int actualBulb1 = LightBulb1.InputPins.GetValue(0);
            int actualBulb2 = LightBulb2.InputPins.GetValue(0);
            int actualBulb3 = LightBulb3.InputPins.GetValue(0);

            // ���ý����ʾԲȦ
            ResultCircle1[i].ChangeImageBasedOnValue(actualBulb1);
            ResultCircle2[i].ChangeImageBasedOnValue(actualBulb2);
            ResultCircle3[i].ChangeImageBasedOnValue(actualBulb3);

            // ������ֵ�Ƿ���Ԥ��ƥ��
            if (bulbExpectedValues[i, 0] == actualBulb1 &&
                bulbExpectedValues[i, 1] == actualBulb2 &&
                bulbExpectedValues[i, 2] == actualBulb3)
            {
                Debug.Log($"��{i + 1}����Գɹ�������ֵΪ X={inputPowerValues[i, 0]}{inputPowerValues[i, 1]}{inputPowerValues[i, 2]}{inputPowerValues[i, 3]}, Y={inputPowerValues[i, 4]}{inputPowerValues[i, 5]}{inputPowerValues[i, 6]}{inputPowerValues[i, 7]}������״̬ƥ�䣡");
            }
            else
            {
                tag = 1;
                Debug.Log($"��{i + 1}�����ʧ�ܣ�����ֵΪ X={inputPowerValues[i, 0]}{inputPowerValues[i, 1]}{inputPowerValues[i, 2]}{inputPowerValues[i, 3]}, Y={inputPowerValues[i, 4]}{inputPowerValues[i, 5]}{inputPowerValues[i, 6]}{inputPowerValues[i, 7]}������״̬��ƥ�䣡");
            }
        }
        if (tag == 1)    // �޸������ tag �ж�
            allPassed = false;
        else
            allPassed = true;
        Debug.Log($"���Խ����{(allPassed ? "���а���ͨ����" : "����ʧ�ܰ�����")}");
    }            
    

}
