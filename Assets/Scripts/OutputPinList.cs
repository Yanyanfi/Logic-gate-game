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
    public void AddPin(int id,Type type,int posX,int posY)
    {
        OutputPin pin = new(id, type, new Vector2Int(posX, posY));
        outputPins.Add(pin);
    }
    public int Count => outputPins.Count;
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
    public void Rotate()
    {
        foreach(var pin in outputPins)
        {
            pin.Rotate();
        }
    }

    // 添加一个通过索引获取输出引脚的方法
    public OutputPin GetPin(int index)
    {
        if (index < 0 || index >= outputPins.Count)
        {
            throw new ArgumentOutOfRangeException("Index out of range");
        }
        return outputPins[index];
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

