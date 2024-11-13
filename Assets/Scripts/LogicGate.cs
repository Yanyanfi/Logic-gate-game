



//***********************已弃用!!!************************//
//***********************已弃用!!!************************//
//***********************已弃用!!!************************//


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;

//public class LogicGate : Component
//{
    
//    // 初始化时，订阅每条输入线路的值改变事件
//    public void SubscribeToInputs()
//    {
//        foreach (var inputWire in inputWires)
//        {
//            if(inputWire!=null)
//                inputWire.ValueChanged += HandleInputChanged; // 订阅输入线路的值改变事件
//        }
//    }
//    public void CancelSubscribeToInputs()
//    {
//        foreach (var inputWire in inputWires)
//        {
//            if (inputWire != null)
//                inputWire.ValueChanged -= HandleInputChanged;
//        }
//    }
//    // 当输入线路的值改变时，重新计算逻辑输出
//    private void HandleInputChanged()
//    {
//        ExecuteLogic(); // 执行逻辑计算
//    }

//    // 设置输出值
//    //protected void SetOutputValue(int value)
//    //{
//    //    outputValue = value;
//    //    if (outputWire != null)
//    //    {
//    //        outputWire.Value=value; // 通过输出线路传递值
//    //    }
//    //}
//    public override void SetPosition(Vector2Int pos)
//    {
//        base.SetPosition(pos);
//    }
//    // 执行逻辑运算（子类实现具体逻辑）
//    protected virtual void ExecuteLogic()
//    {
//        // 子类实现具体逻辑
//    }

//    //在销毁前取消订阅以防止产生异常
//    private void OnDisable()
//    {
//        CancelSubscribeToInputs();
//    }
//}
