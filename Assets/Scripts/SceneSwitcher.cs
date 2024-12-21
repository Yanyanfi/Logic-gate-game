using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        // 检测是否在编辑器中
        #if UNITY_EDITOR
        // 如果是在 Unity 编辑器中运行，停止播放模式
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 如果是在打包后的游戏中运行，退出程序
        Application.Quit();
        #endif

        Debug.Log("Game Quit!"); // 输出调试信息，方便确认退出事件触发
    }
}
