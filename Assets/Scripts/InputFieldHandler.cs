using UnityEngine;
using UnityEngine.UI; // ����UI���

public class InputFieldHandler : MonoBehaviour
{
    // �����UI���
    public InputField inputField;

    // ��ʾ�û�������ı�
    public Text displayText;

    void Start()
    {
        // ȷ����������ʾ�ı���������ȷ��ֵ
        if (inputField != null && displayText != null)
        {
            // ������������ݵĸı��¼�
            inputField.onValueChanged.AddListener(OnInputValueChanged);
        }
    }

    // �����������ݸı�ʱ������
    public void OnInputValueChanged(string value)
    {
        // ��ʾ�û����������
        displayText.text = "�û�����: " + value;
    }

    // ��ѡ�������ťʱ��ӡ����������
    public void OnSubmitButtonClicked()
    {
        string userInput = inputField.text;  // ��ȡ���������
        Debug.Log("�ύ���ı�: " + userInput);
    }
}
