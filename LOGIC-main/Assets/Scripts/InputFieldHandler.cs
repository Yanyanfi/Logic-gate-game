using UnityEngine;
using UnityEngine.UI; // 引用UI组件

public class InputFieldHandler : MonoBehaviour
{
    // 输入框UI组件
    public InputField inputField;

    // 显示用户输入的文本
    public Text displayText;

    void Start()
    {
        // 确保输入框和显示文本对象已正确赋值
        if (inputField != null && displayText != null)
        {
            // 监听输入框内容的改变事件
            inputField.onValueChanged.AddListener(OnInputValueChanged);
        }
    }

    // 当输入框的内容改变时被调用
    public void OnInputValueChanged(string value)
    {
        // 显示用户输入的内容
        displayText.text = "用户输入: " + value;
    }

    // 可选：点击按钮时打印输入框的内容
    public void OnSubmitButtonClicked()
    {
        string userInput = inputField.text;  // 获取输入框内容
        Debug.Log("提交的文本: " + userInput);
    }
}
