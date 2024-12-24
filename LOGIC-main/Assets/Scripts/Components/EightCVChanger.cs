using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightCVChanger : MonoBehaviour
{
    private NewComponent component;

    private List<GameObject> buttons = new List<GameObject>();

    // 创建按钮
    private void CreateButtons()
    {
        // 设置按钮的位置，与引脚的 Y 坐标相同，X 坐标固定为 2
        int[] posY = { 24, 21, 18, 15, 11, 8, 5, 2 };
        int buttonX = 2; // 按钮的 X 坐标

        for (int i = 0; i < posY.Length; i++)
        {
            // 创建按钮 GameObject
            GameObject button = new GameObject($"Button_{i}");
            button.transform.SetParent(transform); // 设置为当前组件的子对象

            // 设置按钮的位置
            button.transform.localPosition = new Vector3(buttonX, posY[i], 0);

            // 添加按钮功能
            var collider = button.AddComponent<BoxCollider>();
            collider.size = new Vector3(2, 2, 1); // 设置碰撞范围

            // 添加翻转值的脚本
            var buttonScript = button.AddComponent<ButtonValueChanger>();
            buttonScript.Component = component;
            buttonScript.PinIndex = i;

            // 添加到按钮列表
            buttons.Add(button);
        }
    }

    private void Awake()
    {
        component = GetComponent<NewComponent>();
        if (component == null)
        {
            Debug.LogError("NewComponent not found on the GameObject. Destroying EightCVChanger.");
            Destroy(this);
            return;
        }

        // 创建八个按钮
        CreateButtons();
    }
}
