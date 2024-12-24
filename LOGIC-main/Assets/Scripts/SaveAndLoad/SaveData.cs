using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<ComponentSaveData> ComponentSaveDatas;
    public List<WireSaveData> WireSaveDatas;

    public SaveData(List<ComponentSaveData> componentSaveDatas, List<WireSaveData> wireSaveDatas)
    {
        ComponentSaveDatas = componentSaveDatas;
        WireSaveDatas = wireSaveDatas;
    }
}
