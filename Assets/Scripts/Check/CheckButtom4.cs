using UnityEngine;
using UnityEngine.UI; // 引入 UI 库以使用按钮
using System.Collections;
using System.Collections.Generic;

public class CheckButtom4 : MonoBehaviour
{
    bool allPassed = false; // 用于标记是否所有案例都通过

    [SerializeField] private NewComponent Power1; // 场景中的电源组件
    [SerializeField] private NewComponent Power2; // 场景中的电源组件
    [SerializeField] private NewComponent Power3; // 场景中的电源组件

    [SerializeField] private LightBulb LightBulb1; // 场景中的灯泡组件
    [SerializeField] private LightBulb LightBulb2; // 场景中的灯泡组件

    [SerializeField] private Button CheckButton;  // 场景中的按钮对象
    [SerializeField] private ResultCircle[] ResultCircle1;  // 场景中的按钮对象
    [SerializeField] private ResultCircle[] ResultCircle2;  // 场景中的按钮对象

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
        if (Power1 == null || Power2 == null || Power3 == null || LightBulb1 == null||LightBulb2 == null)
        {
            Debug.LogWarning("电源或灯泡组件未正确设置！");
            return;
        }
        
        // 电源和灯泡的期望值，分别存储在两个数组中
        int[] powerValues1 = {0,0,0, 0, 1,1,1,1 }; // 电源组件对应的期望值
        int[] powerValues2 = { 0,0, 1,1,0,0,1,1 }; // 电源组件对应的期望值
        int[] powerValues3 = { 0, 1,0,1,0,1,0,1 }; // 电源组件对应的期望值

        int[] bulbValues1 = { 0, 1,1,0,1,0,0,1 };  // 灯泡组件对应的期望值
        int[] bulbValues2 = { 0,0,0, 1,0,1,1,1 };  // 灯泡组件对应的期望值

       // int theOriginPower = Power.OutputPins.GetValue(0);
       // int theOriginBulb = LightBulb.InputPins.GetValue(0);

        int tag = 0;
        for (int i = 0; i < powerValues1.Length; i++)
        {
            // 设置电源的输出值
            Power1.OutputPins.SetValue(0, powerValues1[i]);
            Power2.OutputPins.SetValue(0, powerValues2[i]);
            Power3.OutputPins.SetValue(0, powerValues3[i]);

            // 获取灯泡的输入值
            int theBulbValue1 = LightBulb1.InputPins.GetValue(0);
            int theBulbValue2 = LightBulb2.InputPins.GetValue(0);

            ResultCircle1[i].ChangeImageBasedOnValue(theBulbValue1);
            ResultCircle2[i].ChangeImageBasedOnValue(theBulbValue2);

           // Power.OutputPins.SetValue(0, theOriginPower);
            //LightBulb.InputPins.SetValue(0,theOriginPower);

            // 检查电源和灯泡的值是否匹配
            if (bulbValues1[i] == theBulbValue1&& bulbValues2[i] == theBulbValue2)
            {
                Debug.Log($"检查成功：电源值为 {powerValues1[i]}{powerValues2[i]}{powerValues3[i]}，灯泡值为 {theBulbValue1}{theBulbValue2}，匹配！");
            }
            else
            {
                tag = 1;
                Debug.Log($"检查失败：电源值为 {powerValues1[i]}{powerValues2[i]}{powerValues3[i]}，灯泡值为 {theBulbValue1}{theBulbValue2}，不匹配！");
            }
           
        }
        if(tag == 1)    // 修复这里的 tag 判断
            allPassed = false;
        else
            allPassed = true;
        Debug.Log($"测试结果：{(allPassed ? "所有案例通过！" : "存在失败案例！")}");
    }
}
