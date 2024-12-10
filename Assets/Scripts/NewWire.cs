using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewWire : MonoBehaviour
{
    private int value;
    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            OnValueChanged();
        }
    }
    /// <summary>
    /// 线路两端之间包括两端的所有坐标
    /// </summary>
    public List<Vector2Int> Positions { get; set; }
    public Vector2Int StartPosition { get; set; }
    public Vector2Int TurningPosition { get; set; }
    public Vector2Int EndPosition { get; set; }
    public ValueType Type { get; set; }
    private List<OutputPin> outputPins;
    private List<NewWire> wires;
    public event EventHandler ValueChanged;
    public void OnValueChanged()
    {
        ValueChanged?.Invoke(this,EventArgs.Empty);
    }
    private void Awake()
    {
        outputPins = new();
        wires = new();
        value = 0;
        Type = ValueType.BIT;
    }
    private void HandleValuesOnWiresNotice(object sender,EventArgs e)
    {
        Debug.Log("HandleValuesOnWiresNotice");
        NewWire publisher = sender as NewWire;
        if (Value == publisher.Value)
        {
            Debug.Log("Value == publisher.Value");
            return;
        }

        int tempVal = publisher.Value;
        foreach(var pin in outputPins)
        {
            if (pin == null)
                continue;
            if (pin.Value != tempVal && pin.Value != -2)
            {
                tempVal = -1;
            }
        }
        Value = tempVal;
    }
    private void HandleValuesOnOutputPinsNotice(object sender, EventArgs e)
    {
        OutputPin publisher = sender as OutputPin;
        if (Value == publisher.Value || publisher.Value == -2)
            return;
        int tempVal = publisher.Value;
        foreach (var pin in outputPins)
        {
            if (pin == null)
                continue;
            if (pin.Value != tempVal && pin.Value != -2)
            {
                Value = -1;
            }
            else
            {
                Value = tempVal;
            }
        }
    }

    /// <summary>
    /// 订阅连接到的线和元件的输出引脚的值改变事件
    /// </summary>
    public void SubscribeToWiresAndOutputPins()
    {
        foreach(var wire in wires)
        {
            if (wire != null)
            {
                wire.ValueChanged += HandleValuesOnWiresNotice;
            }
        }
        foreach(var pin in outputPins)
        {
            if (pin != null)
            {
                pin.ValueChanged += HandleValuesOnOutputPinsNotice;
            }
        }
    }

    /// <summary>
    /// 连接某个元件的某个输出引脚
    /// </summary>
    /// <param name="outputPin">输出引脚</param>
    public void Connect(OutputPin outputPin)
    {
        outputPins.Add(outputPin);
    }

    /// <summary>
    /// 连接某条线
    /// </summary>
    /// <param name="wire">线</param>
    public void Connect(NewWire wire)
    {
        wires.Add(wire);
    }

    public void CancelSubscribe()
    {
        foreach(var pin in outputPins)
        {
            if (pin == null)
                continue;
            pin.ValueChanged -= HandleValuesOnOutputPinsNotice;
        }
        foreach(var wire in wires)
        {
            if (wire == null)
                continue;
            wire.ValueChanged -= HandleValuesOnWiresNotice;
        }
    }

    /// <summary>
    /// 断开对其他线的连接；<br/>
    /// 调用该方法后自己依然对其他需要自己的值的对象可见
    /// </summary>
    public void DisConnect()
    {
        CancelSubscribe();
        wires.Clear();
        outputPins.Clear();
    }
    private void OnDestroy()
    {
        DisConnect();
        ValueChanged = null;
    }
}
