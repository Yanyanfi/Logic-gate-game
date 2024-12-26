using UnityEngine;
using UnityEngine.UI;

public class ToggleDescriptionNo : MonoBehaviour
{
    public GameObject descriptionPanel; // ����˵�����
    public Button toggleButton; // ���ð�ť

    private bool isPanelVisible = false; // ��־˵������Ƿ�ɼ�

    void Start()
    {
        // ȷ������ڿ�ʼʱ����
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }

        // �󶨰�ť����¼�
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(TogglePanelVisibility);
        }
    }

    // �л�������ʾ������״̬
    void TogglePanelVisibility()
    {
        isPanelVisible = !isPanelVisible; // ��ת״̬
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(isPanelVisible);
        }
    }
}
