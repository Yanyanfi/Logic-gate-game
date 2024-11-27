using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OutputPinList:IEnumerable<OutputPin>
{
    private List<OutputPin> outputPins=new();

    /// <summary>
    /// 添加一个输出引脚
    /// </summary>
    /// <param name="id">引脚的标识，一般不重复</param>
    /// <param name="type">引脚的类型<br/>一位填：<see cref="Type.BIT"/><br/>八位填：<see cref="Type.BYTE"/></param>
    /// <param name="posX">引脚相对于元件中心在X轴上的偏移</param>
    /// <param name="posY">引脚相对于元件中心在Y轴上的偏移</param>
    public void AddPin(int id,Type type,int posX,int posY)
    {
        OutputPin pin = new(id, type, new Vector2Int(posX, posY));
        outputPins.Add(pin);
    }
    public int Count => outputPins.Count;

    /// <summary>
    /// 所有输出引脚的坐标
    /// </summary>
    public List<Vector2Int> RelativePositions
    {
        get
        {
            List<Vector2Int> result = new();
            foreach(var pin in outputPins)
            {
                result.Add(pin.RelativePosition);
            }
            return result;
        }
    }

    /// <summary>
    /// 设置某个输出引脚的值
    /// </summary>
    /// <param name="id">引脚的标识</param>
    /// <param name="value">要设置的值</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void SetValue(int id,int value)
    {
        var pins = outputPins.Where(pin => pin.Id == id);
        if (!pins.Any())
            throw new InvalidOperationException("Invalid OutputPinList SetValue");
        foreach (var pin in pins)
        {
            pin.Value = value;
        }
    }
    /// <summary>
    /// 获取某个引脚的值
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int GetValue(int id)
    {
        foreach(var pin in outputPins)
        {
            if (pin.Id == id)
            {
                return pin.Value;
            }
        }
        throw new InvalidOperationException("Invalid OutputPinList GetValue");
    }

    public void SubscribeToPins(EventHandler action)
    {
        foreach(var pin in outputPins)
        {
            pin.ValueChanged += action;
        }
    }
    public void Rotate()
    {
        foreach(var pin in outputPins)
        {
            pin.Rotate();
        }
    }

    public IEnumerator<OutputPin> GetEnumerator()
    {
        return ((IEnumerable<OutputPin>)outputPins).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)outputPins).GetEnumerator();
    }
}

