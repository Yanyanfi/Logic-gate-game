using UnityEngine;
using UnityEngine.UI;

public class ResultCircle: MonoBehaviour
{
    [SerializeField] private Image image; // 用来显示图像的 Image 组件
    [SerializeField] private Sprite sprite0; // 值为 0 时的贴图
    [SerializeField] private Sprite sprite1; // 值为 1 时的贴图
/*    [SerializeField] private Sprite spriteDefault; // 值为 1 时的贴图
*/
    // 方法：根据检测值更改图像
    public void ChangeImageBasedOnValue(float value)
    {
        // 判断值，设置对应的贴图
        if (value == 0)
        {
            image.sprite = sprite0; // 显示第一个贴图
        }
        else if (value == 1)
        {
            image.sprite = sprite1; // 显示第二个贴图
        }
        /*else if (value == -1)
        {
            image.sprite = spriteDefault; // 显示第二个贴图
        }*/
    }
}
