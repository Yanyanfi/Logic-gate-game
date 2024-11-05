using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGate : Component
{
    public List<Wire> inputWires = new List<Wire>(); // 输入线路
    public Wire outputWire; // 输出线路
    protected bool outputValue;
    public bool OutputValue { get { return outputValue; } }
    
    // 初始化时，订阅每条输入线路的值改变事件
    public void SubscribeToInputs()
    {
        foreach (var inputWire in inputWires)
        {
            inputWire.OnValueChanged += HandleInputChanged; // 订阅输入线路的值改变事件
        }
    }

    // 当输入线路的值改变时，重新计算逻辑输出
    private void HandleInputChanged()
    {
        ExecuteLogic(); // 执行逻辑计算
    }

    // 设置输出值
    protected void SetOutputValue(bool value)
    {
        outputValue = value;
        if (outputWire != null)
        {
            outputWire.Value=value; // 通过输出线路传递值
        }
    }
    public override void SetPosition(Vector2Int pos)
    {
        base.SetPosition(pos);
    }
    // 执行逻辑运算（子类实现具体逻辑）
    protected virtual void ExecuteLogic()
    {
        // 子类实现具体逻辑
    }
}
