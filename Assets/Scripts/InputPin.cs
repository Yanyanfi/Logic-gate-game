using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InputPin
{
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
    private List<NewWire> wires;
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