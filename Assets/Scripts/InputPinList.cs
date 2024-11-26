using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InputPinList : IEnumerable<InputPin>
{
    private List<InputPin> inputPins = new();
    public InputPin this[int index]
    {
        get
        {
            if (index < 0 || index >= inputPins.Count)
            {
                throw new IndexOutOfRangeException("InputPinList Get IndexOutOfRange");
            }
            return inputPins[index];
        }
    }
    public void AddPin(int id, Type type, int posX, int posY, bool isDelay = false)
    {
        InputPin pin = new(id, type, new Vector2Int(posX, posY), isDelay);
        inputPins.Add(pin);
    }
    public List<Vector2Int> RelativePositions
    {
        get
        {
            List<Vector2Int> result = new();
            foreach(var pin in inputPins)
            {
                if (pin != null)
                {
                    result.Add(pin.RelativePosition);
                }
            }
            return result;
        }
    }
    public int Count => inputPins.Count;
    public void SetPreValues()
    {
        foreach(var pin in inputPins)
        {
            pin?.SetPreValue();
        }
    }
    public int GetValue(int id)
    {
        foreach (var pin in inputPins.Where(pin => pin.Id == id))
        {
            return pin.Value;
        }
        throw new InvalidOperationException("Invalid InputPinList GetValue");
    }
    public bool NoWiresConnected
    {
        get
        {
            foreach(var pin in inputPins)
            {
                if (pin.NoConnectedWires==false)
                    return false;
            }
            return true;
        }
    }
    public void SubscribeToWires(EventHandler action)
    {
        foreach(var pin in inputPins)
        {
            pin.SubscribeToWires(action);
        }
    }
    public void CancelSubscribeToWires(EventHandler action)
    {
        foreach (var pin in inputPins)
        {
            pin.CancelSubscribeToWires(action);
        }
    }
    public void Disconnect(EventHandler action)
    {
        foreach (var pin in inputPins)
        {
            pin.Disconnect(action);
        }
    }
    //顺时针旋转90度
    public void Rotate()
    {
        foreach(var pin in inputPins)
        {
            pin.Rotate();
        }
    }
    public IEnumerator<InputPin> GetEnumerator()
    {
        return ((IEnumerable<InputPin>)inputPins).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)inputPins).GetEnumerator();
    }
}

