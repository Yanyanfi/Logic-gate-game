using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// 包含了编译电路需要的方法
/// </summary>
public class CircuitCompiler : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    /// <summary>
    /// 按以下步骤编译电路;<br/>
    /// 1.清除电路的所有连接。这个步骤包括取消元件引脚、线之间的订阅事件和解除元件和线，线和线之间的引用；<br/>
    /// 2.遍历所有元件和线，根据引脚坐标和线的位置是否重叠，让元件的输入引脚引用线，让线引用输出引脚。<br/>
    ///   根据线的两端坐标是否和其他线重叠。让线之间相互引用；<br/>
    /// 3.调用元件和线的内部方法，让元件和线订阅其引用的对象的值改变事件；<br/>
    /// 4.遍历电路，让所有的元件更新一次输出，所有的线路发布事件。以让电路在初始时刻处于正确状态；
    /// </summary>
    public void Compile()
    {
        if (gridManager != null)
        {
            Debug.LogFormat("not null");
        }
        else
        {
            Debug.LogFormat("is null");
        }
        ClearConnect();
        Connect();
        ComponentsAndWiresSubscribeToInputs();
        InitializeCircuitState();
    }
    private void Connect()
    {
        Debug.LogFormat("Connect start");
        foreach (var component in gridManager.components)
        {
            if (component == null)
                continue;
            foreach (var wire in gridManager.wires)
            {
                if (wire == null)
                {
                    continue;
                }
                foreach(var pin in component.InputPins)
                {
                    if (wire.Positions.Contains(pin.RelativePosition + component.CenterPosition))
                    {
                        pin.Connect(wire);
                    }
                }
                foreach(var pin in component.OutputPins)
                {
                    if (wire.Positions.Contains(pin.RelativePosition + component.CenterPosition))
                    {
                        wire.Connect(pin);
                    }
                }
            }
        }
        for(int i = 0; i < gridManager.wires.Count; i++)
        {
            if (gridManager.wires[i] == null)
                continue;
            for(int j = i + 1; j < gridManager.wires.Count; j++)
            {
                if (gridManager.wires[j] == null)
                    continue;
                NewWire wire1 = gridManager.wires[i];
                NewWire wire2 = gridManager.wires[j];
                if(wire1.Positions.Contains(wire2.StartPosition)||
                   wire1.Positions.Contains(wire2.EndPosition)||
                   wire2.Positions.Contains(wire1.StartPosition)||
                   wire2.Positions.Contains(wire1.EndPosition))
                {
                    //Debug.LogFormat("Connect Wires");
                    wire1.Connect(wire2);
                    wire2.Connect(wire1);
                }
            }
        }
        Debug.LogFormat("Connect finished");
    }
    private void ComponentsAndWiresSubscribeToInputs()
    {
        Debug.LogFormat("SubscribeValues start");
        foreach(var component in gridManager.components)
        {
            if (component == null)
                continue;
            component.SubscribeToInputs();
        }
        foreach(var wire in gridManager.wires)
        {
            if (wire == null)
                continue;
            wire.SubscribeToWiresAndOutputPins();
        }
        Debug.LogFormat("SubscribeValues finished");
    }
    private void InitializeCircuitState()
    {
        Debug.LogFormat("InitializeCircuitState start");
        foreach(var component in gridManager.components)
        {
            component.HandleInputs(null, EventArgs.Empty);
        }
        foreach(var wire in gridManager.wires)
        {
            wire.OnValueChanged();
        }
        Debug.LogFormat("InitializeCircuitState finished");
    }
    private void ClearConnect()
    {
        Debug.LogFormat("ClearConnect start");
        foreach (var wire in gridManager.wires)
        {
            wire.DisConnect();
        }
        foreach (var component in gridManager.components)
        {
            component.Disconnect();
        }
        
        Debug.LogFormat("ClearConnect finished");
    }
}