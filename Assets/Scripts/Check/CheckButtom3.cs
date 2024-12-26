using UnityEngine;
using UnityEngine.UI; // ���� UI ����ʹ�ð�ť
using System.Collections;
using System.Collections.Generic;

public class CheckButtom3 : MonoBehaviour
{
    bool allPassed = false; // ���ڱ���Ƿ����а�����ͨ��
    [SerializeField] private NewComponent Power1; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power2; // �����еĵ�Դ���

    [SerializeField] private LightBulb LightBulb1; // �����еĵ������
    [SerializeField] private LightBulb LightBulb2; // �����еĵ������

    [SerializeField] private Button CheckButton;  // �����еİ�ť����
    [SerializeField] private ResultCircle[] ResultCircle;  // �����еİ�ť����
    [SerializeField] private ResultCircle[] ResultCircle2;  // �����еİ�ť����

    public void CCStart()
    {
        {
            List<string> missingComponents = new List<string>();

            // ����������Ƿ�Ϊ��
            if (Power1 == null) missingComponents.Add("Power1");
            if (Power2 == null) missingComponents.Add("Power2");
            if (LightBulb1 == null) missingComponents.Add("LightBulb1");
            if (LightBulb2 == null) missingComponents.Add("LightBulb2");
            if (CheckButton == null) missingComponents.Add("CheckButton");

            // �����ȱʧ��������������ֹ����
            if (missingComponents.Count > 0)
            {
                Debug.LogWarning($"�������δ��ȷ����: {string.Join(", ", missingComponents)}");
                return;
            }

            // �����߼�
            Debug.Log("�������������ȷ���ã���ʼ����߼�...");
            // �����������߼�����...
        }

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
    
       
        // ��Դ�͵��ݵ�����ֵ���ֱ�洢������������
        int[] power1Values = { 0, 0, 1, 1 }; // ��Դ�����Ӧ������ֵ
        int[] power2Values = { 0, 1, 0, 1 }; // ��Դ�����Ӧ������ֵ

        int[] bulb1Values = { 0, 1, 1, 0 };  // ���������Ӧ������ֵ
        int[] bulb2Values = { 0,  0,0,1 };  // ���������Ӧ������ֵ

        //int theOriginPower1 = Power1.OutputPins.GetValue(0);
        //int theOriginPower2 = Power2.OutputPins.GetValue(0);

        //int theOriginBulb = LightBulb.InputPins.GetValue(0);

        int tag = 0;
        for (int i = 0; i < power1Values.Length; i++)
        {
            // ���õ�Դ�����ֵ
            Power1.OutputPins.SetValue(0, power1Values[i]);
            Power2.OutputPins.SetValue(0, power2Values[i]);

            // ��ȡ���ݵ�����ֵ
            int theBulbValue1 = LightBulb1.InputPins.GetValue(0);
            int theBulbValue2 = LightBulb2.InputPins.GetValue(0);

            ResultCircle[i].ChangeImageBasedOnValue(theBulbValue1);
            ResultCircle2[i].ChangeImageBasedOnValue(theBulbValue2);

           // Power1.OutputPins.SetValue(0, theOriginPower1);
           // Power2.OutputPins.SetValue(0, theOriginPower2);

            //LightBulb.InputPins.SetValue(0, theOriginBulb);

            // ����Դ�͵��ݵ�ֵ�Ƿ�ƥ��
            if (bulb1Values[i] == theBulbValue1 && bulb2Values[i]==theBulbValue2)
            {
                Debug.Log($"���ɹ�����ԴֵΪ {power1Values[i]},{power2Values[i]}������ֵΪ {theBulbValue1}��{theBulbValue2}ƥ�䣡");
            }
            else
            {
                tag = 1;
                Debug.Log($"���ʧ�ܣ���ԴֵΪ {power1Values[i]},{power2Values[i]}������ֵΪ {theBulbValue1}��{theBulbValue2}��ƥ�䣡");
            }

        }
        if (tag == 1)    // �޸������ tag �ж�
            allPassed = false;
        else
            allPassed = true;
        Debug.Log($"���Խ����{(allPassed ? "���а���ͨ����" : "����ʧ�ܰ�����")}");
    }
}
