using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 输入引脚，是元件的组成部分;<br/>
/// 不能单独作为元件的成员属性使用，必须将其放在<see cref="InputPinList"/>容器中间接访问
/// </summary>
public class InputPin
{
    /// <summary>
    /// 由<see cref="InputPinList"/>对象调用的构造函数；<br/>
    /// 在创建一个元件时不会直接使用该构造函数；<br/>
    /// 详情可见<see cref="InputPinList.AddPin(int, Type, int, int, bool)"/>
    /// </summary>
    /// <param name="id">引脚的id,一般不重复</param>
    /// <param name="type">引脚类型：<br/>一位填:<see cref="Type.BIT"/><br/>八位填：<see cref="Type.BYTE"/></param>
    /// <param name="relativePos">引脚相对于中心的坐标</param>
    /// <param name="isDelay">是否延迟一刻</param>
    public InputPin(int id,Type type,Vector2Int relativePos,bool isDelay=false)
    {
        Id = id;
        Type = type;
        RelativePosition = relativePos;
        IsDelay = isDelay;
        wires = new();
        preValue = 0;
    }
    public int Id { get;}
    private List<NewWire> wires;//连接在引脚上的线（可以连接多条线）
    public Type Type { get; }
    public bool IsDelay { get;}//是否延迟一刻
    public bool NoConnectedWires
    {
        get
        {
            foreach(var wire in wires)
            {
                if (wire != null)
                    return false;
            }
            return true;
        }
    }
    private int preValue;
    private int GetCurrentValue()
    {
        int result = 0;
        foreach(var wire in wires)
        {
            if (wire != null)
            {
                result = wire.Value;
                break;
            }
        }
        //短路检查
        foreach(var wire in wires)
        {
            if (wire != null)
            {
                if (result != wire.Value)
                {
                    result = -1;
                    break;
                }
            }
        }
        return result;
    }

    //每个时钟刻在外部被调用一次以保存当前值到下一刻
    public void SetPreValue()
    {
        preValue = GetCurrentValue();
    }
    public void SubscribeToWires(EventHandler action)
    {
        foreach(var wire in wires)
        {
            if (wire != null)
            {
                wire.ValueChanged += action;
            }
        }
    }
    public void CancelSubscribeToWires(EventHandler action)
    {
        foreach(var wire in wires)
        {
            wire.ValueChanged -= action;
        }
    }
    public void Connect(NewWire wire)
    {
        wires.Add(wire);
    }
    public void Disconnect(EventHandler action)
    {
        CancelSubscribeToWires(action);
        wires.Clear();
    }
    public Vector2Int RelativePosition { get; private set; }

    //顺时针旋转90度
    public void Rotate()
    {
        Vector2Int temp = RelativePosition;
        int k = temp.x;
        temp.x = temp.y;
        temp.y = -k;
        RelativePosition = temp;
    }
    public int Value => IsDelay ? preValue : GetCurrentValue();
    
}

public enum Type { BIT, BYTE }