using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ýű���ӵ�����NewComponentԪ��Ԥ�Ƽ��ϼ���ʵ�������ָ�����Ԫ����Χʱ��"R"����ת���ֵ
//����8λ��������ţ��� int ���͵����ĵ�8λ��λȡ��
//�ýű����Power�ű�����ʵ�ֿɿ��ص�Դ�Ĺ���
public class ComponentValueChanger : MonoBehaviour
{
    private NewComponent component;

    private void ChangeValue()
    {
       foreach(var pin in component.OutputPins)
        {
            if (pin.Type == ValueType.BIT)
            {
                pin.Value = pin.Value == 0 ? 1 : 0;
            }
            else
            {
                pin.Value = ~pin.Value & 0xFF;
            }
        }
       
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeValue();
        }
    }
    private void Awake()
    {
        component = GetComponent<NewComponent>();
        if (component == null)
        {
            Destroy(this);
        }
    }
}
