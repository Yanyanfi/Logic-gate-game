using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//将该脚本添加到任意Component元件预制件上即可实现在鼠标指针进入元件范围时按"R"键翻转值
public class ComponentValueChanger : MonoBehaviour
{
    private Component component;

    private void ChangeValue()
    {
        if (component.OutputValue == 1)
        {
            component.OutputValue = 0;
        }
        else if (component.OutputValue == 0)
        {
            component.OutputValue = 1;
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeValue();
        }
    }
    private void Awake()
    {
        component = GetComponent<Component>();
    }
}
