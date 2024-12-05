using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 输入引脚列表<br/>
/// 元件的直接组成部分
/// </summary>
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

    /// <summary>
    /// 添加输入引脚
    /// </summary>
    /// <param name="id">引脚的id，一般不重复</param>
    /// <param name="type">引脚的类型<br/>一位填：<see cref="ValueType.BIT"/><br/>八位填：<see cref="ValueType.BYTE"/></param>
    /// <param name="posX">引脚相对于元件中心在X轴上的偏移</param>
    /// <param name="posY">引脚相对于元件中心在Y轴上的偏移</param>
    /// <param name="isDelay">引脚是否延迟一个时钟刻输出</param>
    public void AddPin(int id, ValueType type, int posX, int posY, bool isDelay = false)
    {
        InputPin pin = new(id, type, new Vector2Int(posX, posY), isDelay);
        inputPins.Add(pin);
    }

    /// <summary>
    /// 所有输入引脚相对于元件中心的位置坐标
    /// </summary>
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

    /// <summary>
    /// 每个时钟刻调用该方法保存所有输入引脚的值到下一刻<br/>
    /// 只有当引脚是延迟类型的，这个方法才存在实际意义；
    /// </summary>
    public void SetPreValues()
    {
        foreach(var pin in inputPins)
        {
            pin?.SetPreValue();
        }
    }
    /// <summary>
    /// 返回某个引脚的值
    /// </summary>
    /// <param name="id">引脚的id</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int GetValue(int id)
    {
        foreach (var pin in inputPins.Where(pin => pin.Id == id))
        {
            return pin.Value;
        }
        throw new InvalidOperationException("Invalid InputPinList GetValue");
    }

    /// <summary>
    /// 所有输入引脚上都没有连线
    /// </summary>
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

    /// <summary>
    /// 向所有输入引脚上的所有连线订阅一个事件<br/>
    /// 一般情况下，该事件就是<see cref="NewComponent.HandleInputs(object, EventArgs)"/>：处理所有输入更新自己的输出
    /// </summary>
    /// <param name="action">一般是<see cref="NewComponent.HandleInputs(object, EventArgs)"/></param>
    public void SubscribeToWires(EventHandler action)
    {
        foreach(var pin in inputPins)
        {
            pin.SubscribeToWires(action);
        }
    }

    /// <summary>
    /// 取消对所有连线的订阅
    /// </summary>
    /// <param name="action">一般是<see cref="NewComponent.HandleInputs(object, EventArgs)"/></param>
    public void CancelSubscribeToWires(EventHandler action)
    {
        foreach (var pin in inputPins)
        {
            pin.CancelSubscribeToWires(action);
        }
    }

    /// <summary>
    /// 断开对所有线的连接（已经包含了取消订阅的步骤）
    /// </summary>
    /// <param name="action">一般是<see cref="NewComponent.HandleInputs(object, EventArgs)"/></param>
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

