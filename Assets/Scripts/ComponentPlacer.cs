using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
/// <summary>
/// 实现生成元件放置元件并让元件在放置前跟随鼠标移动的功能
/// </summary>
public class ComponentPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> componentPrefabs;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float alpha = 0.5f;
    private NewComponent component;
    private GameObject currentComponent;

    void Update()
    {
        if (gridManager.isDragging)
        {
            Vector2Int gridPosition = gridManager.GetGridPosition(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            currentComponent.transform.position = gridManager.GetWorldPosition(gridPosition) + new Vector3(0.5f, 0.5f, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                component.Rotate();
            }
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("GetMouseButtonDown");
                component.SetPositions(gridPosition);
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
    /// <summary>
    /// 调用该方法会从<see cref="componentPrefabs"/>元件预制件列表中根据索引生成一个跟随鼠标移动的元件
    /// </summary>
    /// <param name="index">元件在列表<see cref="componentPrefabs"/>中的索引</param>
    public void StartPlacement(int index)
    {
        if (gridManager.isDragging == false)
        {
            gridManager.isDragging = true;
            gridManager.isDragging = true;
            currentComponent = Instantiate(componentPrefabs[index]);
            component = currentComponent.GetComponent<NewComponent>();
            currentComponent.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            SetChildrenTransparency(alpha);
        }
    }

    //在拖动时设置透明度
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
            Destroy(currentComponent); // 删除元件
            gridManager.isDragging = false;
            currentComponent = null;
        }
    }

}