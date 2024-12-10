using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Load();
    }
    public void Save()
    {
        SaveData data = new(GetComponentSaveDatas(),GetWireSaveDatas());
        string sceneName = SceneManager.GetActiveScene().name;
        // ���л�Ϊ JSON
        string json = JsonUtility.ToJson(data, true);

        // ���屣��·��
        string filePath = Path.Combine(Application.persistentDataPath, sceneName + "_SaveData.json");

        // д���ļ�
        File.WriteAllText(filePath, json);
    }
    public void Load()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string filePath = Path.Combine(Application.persistentDataPath, sceneName + "_SaveData.json");
        if (File.Exists(filePath))
        {
            // ���ļ��ж�ȡ JSON ����
            string json = File.ReadAllText(filePath);

            // �����л� JSON ����Ϊ SaveData ����
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // ����ǰ�����еĶ�̬����
            foreach (var obj in FindObjectsOfType<PrefabInfo>())
            {
                Destroy(obj.gameObject);
            }
            GridManager.Instance.components.Clear();
            GridManager.Instance.wires.Clear();
            LoadComponents(saveData);
            LoadWires(saveData);
        }
        else
        {
            Debug.LogFormat("file does not exist");
        }
    }
    private List<ComponentSaveData> GetComponentSaveDatas()
    {
        List<ComponentSaveData> componentDatas = new();
        foreach (var obj in FindObjectsOfType<NewComponent>())
        {
            if (obj.GetComponent<PrefabInfo>() != null)
                componentDatas.Add(new ComponentSaveData(obj.GetComponent<PrefabInfo>().Name, obj.transform.position, obj.transform.rotation));
        }
        return componentDatas;
    }
    private List<WireSaveData> GetWireSaveDatas()
    {
        List<WireSaveData> wireSaveDatas = new();
        foreach(var obj in FindObjectsOfType<NewWire>())
        {
            wireSaveDatas.Add(new WireSaveData(obj.StartPosition, obj.TurningPosition, obj.EndPosition,obj.Positions));
        }
        return wireSaveDatas;
    }
    private void LoadComponents(SaveData data)
    {
        foreach (var componentData in data.ComponentSaveDatas)
        {
            GameObject prefab = PrefabManager.Instance.GetPrefab(componentData.PrefabName);
            if (prefab != null)
            {
                GameObject instance = Instantiate(prefab,componentData.Position,new Quaternion());
                NewComponent component = instance.GetComponent<NewComponent>();
                if (component == null)
                    Debug.LogFormat("component==null");
                float angle = componentData.Rotation.eulerAngles.z;
                switch (angle)
                {
                    case 90:
                        component.Rotate();
                        component.Rotate();
                        component.Rotate();
                        break;
                    case 180:
                        component.Rotate();
                        component.Rotate();
                        break;
                    case 270:
                        component.Rotate();
                        break;
                    default:
                        break;
                }

                component.SetPositions(GridManager.Instance.GetGridPosition(componentData.Position));
                GridManager.Instance.components.Add(component);
            }
            
        }
    }
    private void LoadWires(SaveData data)
    {
        foreach(var wireData in data.WireSaveDatas)
        {
            GameObject obj = Instantiate(PrefabManager.Instance.GetPrefab("Wire"));
            NewWire wire = obj.GetComponent<NewWire>();
            WireDrawer.Instance.DrawWire(wire, wireData.StartPos, wireData.TurningPos, wireData.EndPos);
            GridManager.Instance.wires.Add(wire);
        }
    }
}
