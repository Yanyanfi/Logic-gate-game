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
        // ����Ƿ��ڱ༭����
        #if UNITY_EDITOR
        // ������� Unity �༭�������У�ֹͣ����ģʽ
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // ������ڴ�������Ϸ�����У��˳�����
        Application.Quit();
        #endif

        Debug.Log("Game Quit!"); // ���������Ϣ������ȷ���˳��¼�����
    }
}
