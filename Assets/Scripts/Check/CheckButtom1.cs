using UnityEngine;
using UnityEngine.UI; // ���� UI ����ʹ�ð�ť
using System.Collections;
using System.Collections.Generic;

public class CheckButtom1 : MonoBehaviour
{
    bool allPassed = false; // ���ڱ���Ƿ����а�����ͨ��

    [SerializeField] private NewComponent Power; // �����еĵ�Դ���
    [SerializeField] private LightBulb LightBulb; // �����еĵ������
    [SerializeField] private Button CheckButton;  // �����еİ�ť����
    [SerializeField] private ResultCircle[] ResultCircle;  // �����еİ�ť����

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
        if (Power == null || LightBulb == null)
        {
            Debug.LogWarning("��Դ��������δ��ȷ���ã�");
            return;
        }
        
        // ��Դ�͵��ݵ�����ֵ���ֱ�洢������������
        int[] powerValues = { 0, 1 }; // ��Դ�����Ӧ������ֵ
        int[] bulbValues = { 0, 1 };  // ���������Ӧ������ֵ

        int theOriginPower = Power.OutputPins.GetValue(0);
        int theOriginBulb = LightBulb.InputPins.GetValue(0);

        int tag = 0;
        for (int i = 0; i < powerValues.Length; i++)
        {
            // ���õ�Դ�����ֵ
            Power.OutputPins.SetValue(0, powerValues[i]);

            // ��ȡ���ݵ�����ֵ
            int theBulbValue = LightBulb.InputPins.GetValue(0);
            ResultCircle[i].ChangeImageBasedOnValue(theBulbValue);
            Power.OutputPins.SetValue(0, theOriginPower);
            //LightBulb.InputPins.SetValue(0,theOriginPower);

            // ����Դ�͵��ݵ�ֵ�Ƿ�ƥ��
            if (bulbValues[i] == theBulbValue)
            {
                Debug.Log($"���ɹ�����ԴֵΪ {powerValues[i]}������ֵΪ {theBulbValue}��ƥ�䣡");
            }
            else
            {
                tag = 1;
                Debug.Log($"���ʧ�ܣ���ԴֵΪ {powerValues[i]}������ֵΪ {theBulbValue}����ƥ�䣡");
            }
           
        }
        if (tag == 1)    // �޸������ tag �ж�
            allPassed = false;
        else
            allPassed = true;
        Debug.Log($"���Խ����{(allPassed ? "���а���ͨ����" : "����ʧ�ܰ�����")}");
    }
}
