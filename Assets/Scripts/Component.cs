using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Component:MonoBehaviour
{
    public List<Wire> inputWires;
    protected int outputValue;
    public int OutputValue
    {
        get
        {
            return outputValue;
        }
        set
        {
            outputValue = value;
            OnValueChanged();
        }
    }
    public event Action ValueChanged;
    public void OnValueChanged()
    {
        ValueChanged?.Invoke();
    }
    // 初始化时，订阅每条输入线路的值改变事件
    public void SubscribeToInputs()
    {
        foreach (var inputWire in inputWires)
        {
            if (inputWire != null)
                inputWire.ValueChanged += HandleInputChanged; // 订阅输入线路的值改变事件
        }
    }
    public void CancelSubscribeToInputs()
    {
        foreach (var inputWire in inputWires)
        {
            if (inputWire != null)
                inputWire.ValueChanged -= HandleInputChanged;
        }
    }
    // 当输入线路的值改变时，重新计算逻辑输出
    private void HandleInputChanged()
    {
        ExecuteLogic(); // 执行逻辑计算
    }
    protected virtual void ExecuteLogic()
    {
        // 子类实现具体逻辑
    }

    //在销毁前取消订阅以防止产生异常
    private void OnDisable()
    {
        CancelSubscribeToInputs();
    }
    //元件除了引脚之外部分占用的坐标
    public List<Vector2Int> PositionOfBody{ get; set; }
    //元件引脚部分占用的坐标
    public List<Vector2Int> PositionOfInputPin { get; set; }
    public List<Vector2Int> PositionOfOutputPin { get; set; }

    //相对于元件中心的坐标
    public List<Vector2Int> RelativePositionOfBody { get; set; }
    public List<Vector2Int> RelativePositionOfInputPin { get; set; }
    public List<Vector2Int> RelativePositionOfOutputPin { get; set; }
    public virtual void SetPosition(Vector2Int pos)
    {

    }
    protected void InitComponent()
    {
        inputWires = new();
        RelativePositionOfInputPin = new();
        RelativePositionOfBody = new();
        RelativePositionOfOutputPin = new();
        PositionOfBody = new();
        PositionOfInputPin = new();
        PositionOfOutputPin = new();
    }
}
