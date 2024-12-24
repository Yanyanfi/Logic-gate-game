using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PrefabInfo : MonoBehaviour
{
    [SerializeField] private string prefabName;
    public string Name => prefabName;
}

