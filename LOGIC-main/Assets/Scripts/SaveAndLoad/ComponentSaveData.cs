using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ComponentSaveData
{
    
    public string PrefabName;
    public Vector3 Position;
    public Quaternion Rotation;

    public ComponentSaveData(string prefabName, Vector3 position, Quaternion rotation)
    {
        PrefabName = prefabName;
        Position = position;
        Rotation = rotation;
    }
}
