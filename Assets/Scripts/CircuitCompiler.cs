using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// �����˱����·��Ҫ�ķ���
/// </summary>
public class CircuitCompiler : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    /// <summary>
    /// �����²�������·;<br/>
    /// 1.�����·���������ӡ�����������ȡ��Ԫ�����š���֮��Ķ����¼��ͽ��Ԫ�����ߣ��ߺ���֮������ã�<br/>
    /// 2.��������Ԫ�����ߣ���������������ߵ�λ���Ƿ��ص�����Ԫ�����������������ߣ���������������š�<br/>
    ///   �����ߵ����������Ƿ���������ص�������֮���໥���ã�<br/>
    /// 3.����Ԫ�����ߵ��ڲ���������Ԫ�����߶��������õĶ����ֵ�ı��¼���<br/>
    /// 4.������·�������е�Ԫ������һ����������е���·�����¼������õ�·�ڳ�ʼʱ�̴�����ȷ״̬��
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