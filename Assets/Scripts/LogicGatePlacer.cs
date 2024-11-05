using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class LogicGatePlacer : MonoBehaviour
{
    [SerializeField] private GameObject logicGatePrefab;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float alpha = 0.5f;
    private GameObject currentLogicGate;

    void Update()
    {
        if (gridManager.isDragging)
        {
            Vector2Int gridPosition = gridManager.GetGridPosition(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            currentLogicGate.transform.position = gridManager.GetWorldPosition(gridPosition) + new Vector3(0.5f, 0.5f, 0);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("asdfs");
                LogicGate logicGate = currentLogicGate.GetComponent<LogicGate>();
                logicGate.SetPosition(gridPosition);
                if (gridManager.CanBePlaced(logicGate))
                {
                    Debug.Log("kefang");
                    gridManager.PlaceLogicGate(logicGate);
                    SetChildrenTransparency(1);
                    gridManager.isDragging = false;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                CancelPlacement();
            }
        }
    }
    public void StartPlacement()
    {
        if (gridManager.isDragging == false)
        {
            gridManager.isDragging = true;
            gridManager.isDragging = true;
            currentLogicGate = Instantiate(logicGatePrefab);
            currentLogicGate.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            SetChildrenTransparency(alpha);
        }   
    }

    private void SetChildrenTransparency(float alpha)
    {
        // 获取所有子对象的 SpriteRenderer（包括父对象本身的 SpriteRenderer）
        SpriteRenderer[] renderers = currentLogicGate.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = alpha;  // 设置透明度
            renderer.color = color;
        }
    }
    private void CancelPlacement()
    {
        if (currentLogicGate != null)
        {
            Destroy(currentLogicGate); // 删除逻辑门对象
            gridManager.isDragging = false;
            currentLogicGate = null;
        }
    }

}
