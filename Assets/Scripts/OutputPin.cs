using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class OutputPin
{
    public OutputPin(int id,Type type,Vector2Int relativePos)
    {
        Id = id;
        Type = type;
        RelativePosition = relativePos;
        value = 0;
    }
    public int Id { get; }
    public Type Type { get; }
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
    public event EventHandler ValueChanged;
    public void OnValueChanged()
    {
        ValueChanged?.Invoke(this,EventArgs.Empty);
    }
    public Vector2Int RelativePosition { get; private set; }
    public void Rotate()
    {
        Vector2Int temp = RelativePosition;
        int k = temp.x;
        temp.x = temp.y;
        temp.y = -k;
        RelativePosition = temp;
    }
}
