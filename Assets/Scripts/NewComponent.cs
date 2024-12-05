using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using System.Xml.Serialization;
using Containers;

/// <summary>
/// 所有电路元件的基类
/// </summary>
public abstract class NewComponent : MonoBehaviour
{
    public Body Body { get; } = new();

    public InputPinList InputPins { get; } = new();
    public OutputPinList OutputPins { get; } = new();
    private AutoExpandList<int> memories = new();
    public bool NoInputWires => InputPins.NoWiresConnected;
    public Vector2Int CenterPosition { get; private set; }
    public List<Vector2Int> PositionsOfBody { get; } = new();//在网格中的绝对位置（包括引脚之外的部分，仅用于在放置元件或画线时检查冲突）
    public List<Vector2Int> PositionsOfPins { get; } = new();//在网格中的绝对位置（包括输入和输出引脚，仅用于在放置元件时检查冲突）
    /// <summary>
    /// 设置元件各个部分的绝对坐标(<see cref="PositionsOfBody"/>和<see cref="PositionsOfPins"/>)
    /// </summary>
    /// <param name="centerPos">元件的中心在网格中的坐标</param>
    public void SetPositions(Vector2Int centerPos)          //依据中心坐标设置各个部分的坐标
    {
        PositionsOfBody.Clear();
        PositionsOfPins.Clear();
        CenterPosition = centerPos;
        foreach (var pos in Body.RelativePositions)
        {
            PositionsOfBody.Add(pos + centerPos);
        }
        foreach (var pos in InputPins.RelativePositions)
        {
            PositionsOfPins.Add(pos + centerPos);
        }
        foreach (var pos in OutputPins.RelativePositions)
        {
            PositionsOfPins.Add(pos + centerPos);
        }
    }
    /// <summary>
    /// 顺时针旋转90度（整个游戏对象）
    /// </summary>
    public void Rotate()
    {
        InputPins.Rotate();
        OutputPins.Rotate();
        Body.Rotate();
        transform.Rotate(0, 0, -90);
    }
    /// <summary>
    /// 逻辑处理方法<br/>
    /// 设置输出引脚的值
    /// <para>
    /// 与非门示例：<see cref="NANDGate.HandleInputs(object, EventArgs)"/>
    /// </para>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public virtual void HandleInputs(object sender, EventArgs e)
    {
        foreach (var pin in OutputPins)
        {
            pin.Value = pin.Value;
        }
    }

    /// <summary>
    /// 初始化元件的各个部分<br/>
    /// 该方法决定元件有哪些引脚、引脚在什么位置以及除了引脚的“身体”的形状<br/>
    /// 在该方法中设置元件的以下属性：<br/>
    /// <see cref="InputPins"/>;<br/>
    /// <see cref="OutputPins"/>;<br/>
    /// <see cref="Body"/>;<br/>
    /// 分别调用方法:<br/>
    /// InputPins.AddPin();<br/>
    /// OutputPins.AddPin();<br/>
    /// Body.AddRelativePosition();
    /// </summary>
    protected abstract void InitShape();

    /// <summary>
    /// 订阅所有连接在输入引脚上的线的值改变事件
    /// </summary>
    public void SubscribeToInputs()
    {
        InputPins.SubscribeToWires(HandleInputs);
    }

    /// <summary>
    /// 断开所有连接到输入引脚上的线（取消订阅+解除引用）
    /// </summary>
    public void Disconnect()
    {
        InputPins.Disconnect(HandleInputs);
    }
    private void Awake()
    {
        InitShape();
    }
    private void OnDestroy()
    {
        Disconnect();
    }
}

    
