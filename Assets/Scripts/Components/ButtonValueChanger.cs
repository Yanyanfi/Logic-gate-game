using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonValueChanger : MonoBehaviour
{
    public NewComponent Component { get; set; }
    public int PinIndex { get; set; } // 对应的引脚索引

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
        if (pin.Type == Type.BIT)
        {
            pin.Value = pin.Value == 0 ? 1 : 0; // 翻转 BIT 引脚的值
        }
        else
        {
            pin.Value = ~pin.Value & 0xFF; // 翻转非 BIT 引脚的低 8 位
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
