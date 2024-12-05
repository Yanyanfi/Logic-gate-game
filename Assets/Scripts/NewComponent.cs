using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using System.Xml.Serialization;
using Containers;

/// <summary>
/// ���е�·Ԫ���Ļ���
/// </summary>
public abstract class NewComponent : MonoBehaviour
{
    public Body Body { get; } = new();

    public InputPinList InputPins { get; } = new();
    public OutputPinList OutputPins { get; } = new();
    private AutoExpandList<int> memories = new();
    public bool NoInputWires => InputPins.NoWiresConnected;
    public Vector2Int CenterPosition { get; private set; }
    public List<Vector2Int> PositionsOfBody { get; } = new();//�������еľ���λ�ã���������֮��Ĳ��֣��������ڷ���Ԫ������ʱ����ͻ��
    public List<Vector2Int> PositionsOfPins { get; } = new();//�������еľ���λ�ã����������������ţ��������ڷ���Ԫ��ʱ����ͻ��
    /// <summary>
    /// ����Ԫ���������ֵľ�������(<see cref="PositionsOfBody"/>��<see cref="PositionsOfPins"/>)
    /// </summary>
    /// <param name="centerPos">Ԫ���������������е�����</param>
    public void SetPositions(Vector2Int centerPos)          //���������������ø������ֵ�����
    {
        PositionsOfBody.Clear();
        PositionsOfPins.Clear();
        CenterPosition = centerPos;
        foreach (var pos in Body.RelativePositions)
        {
            PositionsOfBody.Add(pos + centerPos);
        }
        foreach (var pos in InputPins.RelativePositions)
        {
            PositionsOfPins.Add(pos + centerPos);
        }
        foreach (var pos in OutputPins.RelativePositions)
        {
            PositionsOfPins.Add(pos + centerPos);
        }
    }
    /// <summary>
    /// ˳ʱ����ת90�ȣ�������Ϸ����
    /// </summary>
    public void Rotate()
    {
        InputPins.Rotate();
        OutputPins.Rotate();
        Body.Rotate();
        transform.Rotate(0, 0, -90);
    }
    /// <summary>
    /// �߼�������<br/>
    /// ����������ŵ�ֵ
    /// <para>
    /// �����ʾ����<see cref="NANDGate.HandleInputs(object, EventArgs)"/>
    /// </para>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public virtual void HandleInputs(object sender, EventArgs e)
    {
        foreach (var pin in OutputPins)
        {
            pin.Value = pin.Value;
        }
    }

    /// <summary>
    /// ��ʼ��Ԫ���ĸ�������<br/>
    /// �÷�������Ԫ������Щ���š�������ʲôλ���Լ��������ŵġ����塱����״<br/>
    /// �ڸ÷���������Ԫ�����������ԣ�<br/>
    /// <see cref="InputPins"/>;<br/>
    /// <see cref="OutputPins"/>;<br/>
    /// <see cref="Body"/>;<br/>
    /// �ֱ���÷���:<br/>
    /// InputPins.AddPin();<br/>
    /// OutputPins.AddPin();<br/>
    /// Body.AddRelativePosition();
    /// </summary>
    protected abstract void InitShape();

    /// <summary>
    /// �����������������������ϵ��ߵ�ֵ�ı��¼�
    /// </summary>
    public void SubscribeToInputs()
    {
        InputPins.SubscribeToWires(HandleInputs);
    }

    /// <summary>
    /// �Ͽ��������ӵ����������ϵ��ߣ�ȡ������+������ã�
    /// </summary>
    public void Disconnect()
    {
        InputPins.Disconnect(HandleInputs);
    }
    private void Awake()
    {
        InitShape();
    }
    private void OnDestroy()
    {
        Disconnect();
    }
}

    
