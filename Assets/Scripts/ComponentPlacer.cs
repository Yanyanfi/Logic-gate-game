using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ComponentPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> componentPrefabs;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float alpha = 0.5f;
    private GameObject currentComponent;

    void Update()
    {
        if (gridManager.isDragging)
        {
            Vector2Int gridPosition = gridManager.GetGridPosition(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            currentComponent.transform.position = gridManager.GetWorldPosition(gridPosition) + new Vector3(0.5f, 0.5f, 0);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("GetMouseButtonDown");
                Component component = currentComponent.GetComponent<Component>();
                component.SetPosition(gridPosition);
                if (gridManager.CanBePlaced(component))
                {
                    Debug.Log("CanBePlaced");
                    gridManager.PlaceComponent(component);
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
    public void StartPlacement(int index)
    {
        if (gridManager.isDragging == false)
        {
            gridManager.isDragging = true;
            gridManager.isDragging = true;
            currentComponent = Instantiate(componentPrefabs[index]);
            currentComponent.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            SetChildrenTransparency(alpha);
        }
    }

    private void SetChildrenTransparency(float alpha)
    {
        // 获取所有子对象的 SpriteRenderer（包括父对象本身的 SpriteRenderer）
        SpriteRenderer[] renderers = currentComponent.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = alpha;  // 设置透明度
            renderer.color = color;
        }
    }
    private void CancelPlacement()
    {
        if (currentComponent != null)
        {
            Destroy(currentComponent); // 删除逻辑门对象
            gridManager.isDragging = false;
            currentComponent = null;
        }
    }

}
