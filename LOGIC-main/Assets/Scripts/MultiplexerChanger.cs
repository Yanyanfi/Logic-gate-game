using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplexerChanger : MonoBehaviour
{
    private NewComponent component;

    // 三种选择信号：0、1、2
    private int selectSignal = 0; // 初始选择信号为0 (表示状态 0)

    // 控制选择信号变化（0 -> 1 -> 2 -> 0 ...）
    private void ChangeSelectSignal()
    {
        selectSignal = (selectSignal + 1) % 3; // 0, 1, 2 循环
    }

    // 更新输出引脚的值，根据选择信号的值
    private void ChangeValue()
    {
        // 获取第一个输出引脚（假设只有一个输出引脚）
        var outputPin = component.OutputPins[0]; // 使用 GetPin 来获取引脚

        // 根据选择信号修改输出引脚的值
        // 选择信号为 0，设置输出为 0
        // 选择信号为 1，设置输出为 1
        // 选择信号为 2，设置输出为 2
        outputPin.Value = selectSignal;
    }

    // 当鼠标指针悬停在组件上，并按下 "E" 键时调用
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeSelectSignal(); // 切换选择信号
            ChangeValue(); // 更新输出引脚的值
        }
    }

    private void Awake()
    {
        component = GetComponent<NewComponent>();
        if (component == null)
        {
            Destroy(this); // 如果没有找到 NewComponent，销毁该脚本
        }
    }
}
