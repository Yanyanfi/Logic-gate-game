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
    public List<Vector2Int> Positions { get; set; }
    public Vector2Int StartPosition { get; set; }
    public Vector2Int EndPosition { get; set; }
    public Type Type { get; set; }
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
        Type = Type.BIT;
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
    public void NoticeOtherWiresToUpdate()
    {
        Value = Value;
    }
    public void Connect(OutputPin outputPin)
    {
        outputPins.Add(outputPin);
    }
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
