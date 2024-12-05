using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private List<ComponentSaveData> componentDatas=new();
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
    }
    public void Save()
    {
        componentDatas.Clear();
        foreach(var componentPrefab in FindObjectsOfType<NewComponent>())
        {
            Vector3 position = componentPrefab.transform.position;
            Vector3 rotation = componentPrefab.transform.rotation.eulerAngles;
            string name = componentPrefab.name;
        }
    }
}
