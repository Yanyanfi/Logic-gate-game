using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using System.Xml.Serialization;

public abstract class NewComponent : MonoBehaviour
{
    public Body Body { get; } = new();
    
    public InputPinList InputPins { get; } = new();
    public OutputPinList OutputPins { get; } = new();
    public bool NoInputWires => InputPins.NoWiresConnected;
    public Vector2Int CenterPosition { get; private set; }
    public List<Vector2Int> PositionsOfBody { get; } = new();//在网格中的绝对位置（包括引脚之外的部分，仅用于在放置元件或画线时检查冲突）
    public List<Vector2Int> PositionsOfPins { get; } = new();//在网格中的绝对位置（包括输入和输出引脚，仅用于在放置元件时检查冲突）
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
    //顺时针旋转90度（整个游戏对象）
    public void Rotate()
    {
        InputPins.Rotate();
        OutputPins.Rotate();
        Body.Rotate();
        transform.Rotate(0, 0, -90);
    }
    public virtual void HandleInputs(object sender,EventArgs e)
    {
        foreach(var pin in OutputPins)
        {
            pin.Value = pin.Value;
        }
    }
    protected abstract void InitShape(); 
    public void SubscribeToInputs()
    {
        InputPins.SubscribeToWires(HandleInputs);
    }
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
