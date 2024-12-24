using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, GameObject> popupObjects;
    public void ShowPopup(string popupName)
    {
        if (popupObjects.Keys.Contains(popupName))
        {
            popupObjects[popupName].SetActive(true);
        }
    }
    public void HidePopup(string popupName)
    {
        if (popupObjects.Keys.Contains(popupName))
        {
            popupObjects[popupName].SetActive(false);
        }
    }
}