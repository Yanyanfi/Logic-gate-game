using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 输出引脚，是元件的组成部分;<br/>
/// 不能单独作为元件的成员属性使用，必须将其放在<see cref="OutputPinList"/>容器中间接访问
/// </summary>
public class OutputPin
{
    public OutputPin(int id,ValueType type,Vector2Int relativePos)
    {
        Id = id;
        Type = type;
        RelativePosition = relativePos;
        value = 0;
    }
    public int Id { get; }
    public ValueType Type { get; }
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
