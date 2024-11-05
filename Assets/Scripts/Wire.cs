using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire:MonoBehaviour
{
    public List<Vector2Int> Position { get; set; }
    public Vector2Int StartPos { get; set; }
    public Vector2Int EndPos { get; set; }
    private bool value; // 存储线路的当前值
    public bool Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            OnValueChanged?.Invoke();
        }
    }
    // 定义值改变事件
    public event Action OnValueChanged;

    private void Awake()
    {
        Position = new();
        StartPos = new();
        EndPos = new();
        value = false;
    }
}
