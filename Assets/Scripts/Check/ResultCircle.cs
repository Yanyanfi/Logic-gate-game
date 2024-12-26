using UnityEngine;
using UnityEngine.UI;

public class ResultCircle: MonoBehaviour
{
    [SerializeField] private Image image; // ������ʾͼ��� Image ���
    [SerializeField] private Sprite sprite0; // ֵΪ 0 ʱ����ͼ
    [SerializeField] private Sprite sprite1; // ֵΪ 1 ʱ����ͼ
/*    [SerializeField] private Sprite spriteDefault; // ֵΪ 1 ʱ����ͼ
*/
    // ���������ݼ��ֵ����ͼ��
    public void ChangeImageBasedOnValue(float value)
    {
        // �ж�ֵ�����ö�Ӧ����ͼ
        if (value == 0)
        {
            image.sprite = sprite0; // ��ʾ��һ����ͼ
        }
        else if (value == 1)
        {
            image.sprite = sprite1; // ��ʾ�ڶ�����ͼ
        }
        /*else if (value == -1)
        {
            image.sprite = spriteDefault; // ��ʾ�ڶ�����ͼ
        }*/
    }
}
