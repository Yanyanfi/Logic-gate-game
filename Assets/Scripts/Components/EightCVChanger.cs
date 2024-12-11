using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightCVChanger : MonoBehaviour
{
    private NewComponent component;

    private List<GameObject> buttons = new List<GameObject>();

    // ������ť
    private void CreateButtons()
    {
        // ���ð�ť��λ�ã������ŵ� Y ������ͬ��X ����̶�Ϊ 2
        int[] posY = { 24, 21, 18, 15, 11, 8, 5, 2 };
        int buttonX = 2; // ��ť�� X ����

        for (int i = 0; i < posY.Length; i++)
        {
            // ������ť GameObject
            GameObject button = new GameObject($"Button_{i}");
            button.transform.SetParent(transform); // ����Ϊ��ǰ������Ӷ���

            // ���ð�ť��λ��
            button.transform.localPosition = new Vector3(buttonX, posY[i], 0);

            // ��Ӱ�ť����
            var collider = button.AddComponent<BoxCollider>();
            collider.size = new Vector3(2, 2, 1); // ������ײ��Χ

            // ��ӷ�תֵ�Ľű�
            var buttonScript = button.AddComponent<ButtonValueChanger>();
            buttonScript.Component = component;
            buttonScript.PinIndex = i;

            // ��ӵ���ť�б�
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

        // �����˸���ť
        CreateButtons();
    }
}
