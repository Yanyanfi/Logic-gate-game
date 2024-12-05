using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//将该脚本添加到任意NewComponent元件预制件上即可实现在鼠标指针进入元件范围时按"R"键翻转输出值
//对于8位的输出引脚，对 int 类型的数的低8位按位取反
//该脚本配合Power脚本可以实现可开关电源的功能
public class ComponentValueChanger : MonoBehaviour
{
    private NewComponent component;

    private void ChangeValue()
    {
       foreach(var pin in component.OutputPins)
        {
            if (pin.Type == ValueType.BIT)
            {
                pin.Value = pin.Value == 0 ? 1 : 0;
            }
            else
            {
                pin.Value = ~pin.Value & 0xFF;
            }
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
        component = GetComponent<NewComponent>();
        if (component == null)
        {
            Destroy(this);
        }
    }
}
