using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonValueChanger : MonoBehaviour
{
    public NewComponent Component { get; set; }
    public int PinIndex { get; set; } // ��Ӧ����������

    private void ChangeValue()
    {
        if (Component == null)
        {
            Debug.LogError("Component is not set in ButtonValueChanger.");
            return;
        }

        if (PinIndex < 0 || PinIndex >= Component.OutputPins.Count)
        {
            Debug.LogError($"Invalid PinIndex: {PinIndex}. OutputPins count: {Component.OutputPins.Count}.");
            return;
        }

        var pin = Component.OutputPins[PinIndex];
        if (pin.Type == ValueType.BIT)
        {
            pin.Value = pin.Value == 0 ? 1 : 0; // ��ת BIT ���ŵ�ֵ
        }
        else
        {
            pin.Value = ~pin.Value & 0xFF; // ��ת�� BIT ���ŵĵ� 8 λ
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeValue();
        }
    }
}
