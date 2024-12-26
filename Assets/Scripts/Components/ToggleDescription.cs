using UnityEngine;
using UnityEngine.UI;

public class ToggleDescription : MonoBehaviour
{
    public GameObject descriptionPanel; // ����˵�����
    public Button toggleButton; // ���ð�ť

    private bool isPanelVisible = true; // ��־˵������Ƿ�ɼ�

    void Start()
    {
        // ȷ������ڿ�ʼʱ����
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(true);
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
