using UnityEngine;
using UnityEngine.UI;

public class ToggleDescription : MonoBehaviour
{
    public GameObject descriptionPanel; // 引用说明面板
    public Button toggleButton; // 引用按钮

    private bool isPanelVisible = true; // 标志说明面板是否可见

    void Start()
    {
        // 确保面板在开始时隐藏
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(true);
        }

        // 绑定按钮点击事件
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(TogglePanelVisibility);
        }
    }

    // 切换面板的显示和隐藏状态
    void TogglePanelVisibility()
    {
        isPanelVisible = !isPanelVisible; // 反转状态
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(isPanelVisible);
        }
    }
}
