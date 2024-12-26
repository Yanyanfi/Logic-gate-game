using UnityEngine;
using UnityEngine.UI; // ���� UI ����ʹ�ð�ť
using System.Collections;
using System.Collections.Generic;

public class CheckButtom4 : MonoBehaviour
{
    bool allPassed = false; // ���ڱ���Ƿ����а�����ͨ��

    [SerializeField] private NewComponent Power1; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power2; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power3; // �����еĵ�Դ���

    [SerializeField] private LightBulb LightBulb1; // �����еĵ������
    [SerializeField] private LightBulb LightBulb2; // �����еĵ������

    [SerializeField] private Button CheckButton;  // �����еİ�ť����
    [SerializeField] private ResultCircle[] ResultCircle1;  // �����еİ�ť����
    [SerializeField] private ResultCircle[] ResultCircle2;  // �����еİ�ť����

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
        if (Power1 == null || Power2 == null || Power3 == null || LightBulb1 == null||LightBulb2 == null)
        {
            Debug.LogWarning("��Դ��������δ��ȷ���ã�");
            return;
        }
        
        // ��Դ�͵��ݵ�����ֵ���ֱ�洢������������
        int[] powerValues1 = {0,0,0, 0, 1,1,1,1 }; // ��Դ�����Ӧ������ֵ
        int[] powerValues2 = { 0,0, 1,1,0,0,1,1 }; // ��Դ�����Ӧ������ֵ
        int[] powerValues3 = { 0, 1,0,1,0,1,0,1 }; // ��Դ�����Ӧ������ֵ

        int[] bulbValues1 = { 0, 1,1,0,1,0,0,1 };  // ���������Ӧ������ֵ
        int[] bulbValues2 = { 0,0,0, 1,0,1,1,1 };  // ���������Ӧ������ֵ

       // int theOriginPower = Power.OutputPins.GetValue(0);
       // int theOriginBulb = LightBulb.InputPins.GetValue(0);

        int tag = 0;
        for (int i = 0; i < powerValues1.Length; i++)
        {
            // ���õ�Դ�����ֵ
            Power1.OutputPins.SetValue(0, powerValues1[i]);
            Power2.OutputPins.SetValue(0, powerValues2[i]);
            Power3.OutputPins.SetValue(0, powerValues3[i]);

            // ��ȡ���ݵ�����ֵ
            int theBulbValue1 = LightBulb1.InputPins.GetValue(0);
            int theBulbValue2 = LightBulb2.InputPins.GetValue(0);

            ResultCircle1[i].ChangeImageBasedOnValue(theBulbValue1);
            ResultCircle2[i].ChangeImageBasedOnValue(theBulbValue2);

           // Power.OutputPins.SetValue(0, theOriginPower);
            //LightBulb.InputPins.SetValue(0,theOriginPower);

            // ����Դ�͵��ݵ�ֵ�Ƿ�ƥ��
            if (bulbValues1[i] == theBulbValue1&& bulbValues2[i] == theBulbValue2)
            {
                Debug.Log($"���ɹ�����ԴֵΪ {powerValues1[i]}{powerValues2[i]}{powerValues3[i]}������ֵΪ {theBulbValue1}{theBulbValue2}��ƥ�䣡");
            }
            else
            {
                tag = 1;
                Debug.Log($"���ʧ�ܣ���ԴֵΪ {powerValues1[i]}{powerValues2[i]}{powerValues3[i]}������ֵΪ {theBulbValue1}{theBulbValue2}����ƥ�䣡");
            }
           
        }
        if(tag == 1)    // �޸������ tag �ж�
            allPassed = false;
        else
            allPassed = true;
        Debug.Log($"���Խ����{(allPassed ? "���а���ͨ����" : "����ʧ�ܰ�����")}");
    }
}
