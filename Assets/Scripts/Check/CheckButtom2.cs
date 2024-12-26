using UnityEngine;
using UnityEngine.UI; // ���� UI ����ʹ�ð�ť
using System.Collections;
using System.Collections.Generic;

public class CheckButtom2 : MonoBehaviour
{
    bool allPassed = false; // ���ڱ���Ƿ����а�����ͨ��

    [SerializeField] private NewComponent Power1; // �����еĵ�Դ���
    [SerializeField] private NewComponent Power2; // �����еĵ�Դ���

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
        if (Power1 == null||Power2 == null || LightBulb == null)
        {
            Debug.LogWarning("��Դ��������δ��ȷ���ã�");
            return;
        }

        // ��Դ�͵��ݵ�����ֵ���ֱ�洢������������
        int[] power1Values = { 0,0,1, 1 }; // ��Դ�����Ӧ������ֵ
        int[] power2Values = { 0, 1,0,1 }; // ��Դ�����Ӧ������ֵ

        int[] bulbValues = { 0,1,1, 0 };  // ���������Ӧ������ֵ

        int theOriginPower1 = Power1.OutputPins.GetValue(0);
        int theOriginPower2 = Power2.OutputPins.GetValue(0);

        int theOriginBulb = LightBulb.InputPins.GetValue(0);

        int tag = 0;
        for (int i = 0; i < power1Values.Length; i++)
        {
            // ���õ�Դ�����ֵ
            Power1.OutputPins.SetValue(0, power1Values[i]);
            Power2.OutputPins.SetValue(0, power2Values[i]);

            // ��ȡ���ݵ�����ֵ
            int theBulbValue = LightBulb.InputPins.GetValue(0);
            ResultCircle[i].ChangeImageBasedOnValue(theBulbValue);
            Power1.OutputPins.SetValue(0, theOriginPower1);
            Power2.OutputPins.SetValue(0, theOriginPower2);

            //LightBulb.InputPins.SetValue(0, theOriginBulb);

            // ����Դ�͵��ݵ�ֵ�Ƿ�ƥ��
            if (bulbValues[i] == theBulbValue)
            {
                Debug.Log($"���ɹ�����ԴֵΪ {power1Values[i]},{power2Values[i]}������ֵΪ {theBulbValue}��ƥ�䣡");
            }
            else
            {
                tag = 1;
                Debug.Log($"���ʧ�ܣ���ԴֵΪ {power1Values[i]},{power2Values[i]}������ֵΪ {theBulbValue}����ƥ�䣡");
            }

        }
        if (tag == 1)    // �޸������ tag �ж�
            allPassed = false;
        else
            allPassed = true;
        Debug.Log($"���Խ����{(allPassed ? "���а���ͨ����" : "����ʧ�ܰ�����")}");
    }
}
