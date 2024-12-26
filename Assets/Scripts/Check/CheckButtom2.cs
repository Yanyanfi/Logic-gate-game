using UnityEngine;
using UnityEngine.UI; // 引入 UI 库以使用按钮
using System.Collections;
using System.Collections.Generic;

public class CheckButtom2 : MonoBehaviour
{
    bool allPassed = false; // 用于标记是否所有案例都通过

    [SerializeField] private NewComponent Power1; // 场景中的电源组件
    [SerializeField] private NewComponent Power2; // 场景中的电源组件

    [SerializeField] private LightBulb LightBulb; // 场景中的灯泡组件
    [SerializeField] private Button CheckButton;  // 场景中的按钮对象
    [SerializeField] private ResultCircle[] ResultCircle;  // 场景中的按钮对象

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
        if (Power1 == null||Power2 == null || LightBulb == null)
        {
            Debug.LogWarning("电源或灯泡组件未正确设置！");
            return;
        }

        // 电源和灯泡的期望值，分别存储在两个数组中
        int[] power1Values = { 0,0,1, 1 }; // 电源组件对应的期望值
        int[] power2Values = { 0, 1,0,1 }; // 电源组件对应的期望值

        int[] bulbValues = { 0,1,1, 0 };  // 灯泡组件对应的期望值

        int theOriginPower1 = Power1.OutputPins.GetValue(0);
        int theOriginPower2 = Power2.OutputPins.GetValue(0);

        int theOriginBulb = LightBulb.InputPins.GetValue(0);

        int tag = 0;
        for (int i = 0; i < power1Values.Length; i++)
        {
            // 设置电源的输出值
            Power1.OutputPins.SetValue(0, power1Values[i]);
            Power2.OutputPins.SetValue(0, power2Values[i]);

            // 获取灯泡的输入值
            int theBulbValue = LightBulb.InputPins.GetValue(0);
            ResultCircle[i].ChangeImageBasedOnValue(theBulbValue);
            Power1.OutputPins.SetValue(0, theOriginPower1);
            Power2.OutputPins.SetValue(0, theOriginPower2);

            //LightBulb.InputPins.SetValue(0, theOriginBulb);

            // 检查电源和灯泡的值是否匹配
            if (bulbValues[i] == theBulbValue)
            {
                Debug.Log($"检查成功：电源值为 {power1Values[i]},{power2Values[i]}，灯泡值为 {theBulbValue}，匹配！");
            }
            else
            {
                tag = 1;
                Debug.Log($"检查失败：电源值为 {power1Values[i]},{power2Values[i]}，灯泡值为 {theBulbValue}，不匹配！");
            }

        }
        if (tag == 1)    // 修复这里的 tag 判断
            allPassed = false;
        else
            allPassed = true;
        Debug.Log($"测试结果：{(allPassed ? "所有案例通过！" : "存在失败案例！")}");
    }
}
