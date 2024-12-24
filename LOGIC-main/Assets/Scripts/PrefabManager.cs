using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    public static PrefabManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject GetPrefab(string name)
    {
        foreach(var prefab in prefabs)
        {
            var info = prefab.GetComponent<PrefabInfo>();
            if (info == null)
                continue;
            if (info.Name == name)
            {
                return prefab;
            }
        }
        return null;
    }
}
