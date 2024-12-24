using UnityEngine;
using UnityEngine.UI; // 引入 UI 库以使用按钮
using System.Collections;
using System.Collections.Generic;

public class CheckButtom5 : MonoBehaviour
{
    bool allPassed = false ; // 用于标记是否所有案例都通过

    [SerializeField] private NewComponent Power1; // 场景中的电源组件
    [SerializeField] private NewComponent Power2; // 场景中的电源组件
    [SerializeField] private NewComponent Power3; // 场景中的电源组件
    [SerializeField] private NewComponent Power4; // 场景中的电源组件
    [SerializeField] private NewComponent Power5; // 场景中的电源组件
    [SerializeField] private NewComponent Power6; // 场景中的电源组件
    [SerializeField] private NewComponent Power7; // 场景中的电源组件
    [SerializeField] private NewComponent Power8; // 场景中的电源组件

    [SerializeField] private LightBulb LightBulb1; // 场景中的灯泡组件
    [SerializeField] private LightBulb LightBulb2; // 场景中的灯泡组件
    [SerializeField] private LightBulb LightBulb3; // 场景中的灯泡组件

    [SerializeField] private Button CheckButton;  // 场景中的按钮对象
    [SerializeField] private ResultCircle[] ResultCircle1;  // 场景中的按钮对象
    [SerializeField] private ResultCircle[] ResultCircle2;  // 场景中的按钮对象
    [SerializeField] private ResultCircle[] ResultCircle3;  // 场景中的按钮对象

    public void CCStart()
    {
        if (CheckButton != null)
        {
            // 为按钮绑定点击事件
            CheckButton.onClick.AddListener(CheckAndSetPower);
        }
        else
        {
            Debug.LogWarning("按钮未正确设置！");
        }
    }

    private void CheckAndSetPower()
    {
        if (Power1 == null || Power2 == null || Power3 == null || Power4 == null ||
            Power5 == null || Power6 == null || Power7 == null || Power8 == null ||
            LightBulb1 == null || LightBulb2 == null || LightBulb3 == null)
        {
            Debug.LogWarning("电源或灯泡组件未正确设置！");
            return;
        }

        // 输入值：前四位代表 X，后四位代表 Y
        int[,] inputPowerValues = {
            { 0, 0, 0, 0, 0, 0, 0, 0 },  // 第一组测试数据
            { 0, 1, 1, 1, 1, 0, 0, 0 },  // 第二组测试数据
            { 1, 0, 0, 0, 0, 1, 1, 1 }   // 第三组测试数据
        };

        // 灯泡的期望输出值
        int[,] bulbExpectedValues = {
            { 1, 0, 0 },  // 第一组测试数据灯泡状态
            { 0, 1, 1 },  // 第二组测试数据灯泡状态
            { 0, 0, 1 }   // 第三组测试数据灯泡状态
        };
        int tag = 0;
        for (int i = 0; i < inputPowerValues.GetLength(0); i++)
        {
            // 为电源组件设置当前的测试输入值
            Power1.OutputPins.SetValue(0, inputPowerValues[i, 0]);
            Power2.OutputPins.SetValue(0, inputPowerValues[i, 1]);
            Power3.OutputPins.SetValue(0, inputPowerValues[i, 2]);
            Power4.OutputPins.SetValue(0, inputPowerValues[i, 3]);

            Power5.OutputPins.SetValue(0, inputPowerValues[i, 4]);
            Power6.OutputPins.SetValue(0, inputPowerValues[i, 5]);
            Power7.OutputPins.SetValue(0, inputPowerValues[i, 6]);
            Power8.OutputPins.SetValue(0, inputPowerValues[i, 7]);

            // 获取灯泡的实际输出值
            int actualBulb1 = LightBulb1.InputPins.GetValue(0);
            int actualBulb2 = LightBulb2.InputPins.GetValue(0);
            int actualBulb3 = LightBulb3.InputPins.GetValue(0);

            // 设置结果显示圆圈
            ResultCircle1[i].ChangeImageBasedOnValue(actualBulb1);
            ResultCircle2[i].ChangeImageBasedOnValue(actualBulb2);
            ResultCircle3[i].ChangeImageBasedOnValue(actualBulb3);

            // 检查灯泡值是否与预期匹配
            if (bulbExpectedValues[i, 0] == actualBulb1 &&
                bulbExpectedValues[i, 1] == actualBulb2 &&
                bulbExpectedValues[i, 2] == actualBulb3)
            {
                Debug.Log($"第{i + 1}组测试成功：输入值为 X={inputPowerValues[i, 0]}{inputPowerValues[i, 1]}{inputPowerValues[i, 2]}{inputPowerValues[i, 3]}, Y={inputPowerValues[i, 4]}{inputPowerValues[i, 5]}{inputPowerValues[i, 6]}{inputPowerValues[i, 7]}，灯泡状态匹配！");
            }
            else
            {
                tag = 1;
                Debug.Log($"第{i + 1}组测试失败：输入值为 X={inputPowerValues[i, 0]}{inputPowerValues[i, 1]}{inputPowerValues[i, 2]}{inputPowerValues[i, 3]}, Y={inputPowerValues[i, 4]}{inputPowerValues[i, 5]}{inputPowerValues[i, 6]}{inputPowerValues[i, 7]}，灯泡状态不匹配！");
            }
        }
        if (tag == 1)    // 修复这里的 tag 判断
            allPassed = false;
        else
            allPassed = true;
        Debug.Log($"测试结果：{(allPassed ? "所有案例通过！" : "存在失败案例！")}");
    }            
    

}
