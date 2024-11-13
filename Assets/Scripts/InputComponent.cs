using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputComponent : MonoBehaviour
{
    protected int outputValue;
    public int OutputValue
    {
        get
        {
            return outputValue;
        }
        set
        {
            outputValue = value;
            OnValueChanged();
        }
    }
    public List<Vector2Int> PositionOfBody { get; set; }
    public List<Vector2Int> PositionOfOutputPin { get; set; }
    public event Action ValueChanged;
    public void OnValueChanged()
    {
        ValueChanged?.Invoke();
    }
    public virtual void SetPosition(Vector2Int pos){ }
}
