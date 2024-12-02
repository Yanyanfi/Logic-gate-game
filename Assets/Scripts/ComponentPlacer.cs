using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// ʵ������Ԫ������Ԫ������Ԫ���ڷ���ǰ��������ƶ��Ĺ���
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
            if (Input.GetMouseButtonDown(0)&& EventSystem.current.IsPointerOverGameObject()==false)
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
    /// ���ø÷������<see cref="componentPrefabs"/>Ԫ��Ԥ�Ƽ��б��и�����������һ����������ƶ���Ԫ��
    /// </summary>
    /// <param name="index">Ԫ�����б�<see cref="componentPrefabs"/>�е�����</param>
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

    //���϶�ʱ����͸����
    private void SetChildrenTransparency(float alpha)
    {
        // ��ȡ�����Ӷ���� SpriteRenderer��������������� SpriteRenderer��
        SpriteRenderer[] renderers = currentComponent.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = alpha;  // ����͸����
            renderer.color = color;
        }
    }
    private void CancelPlacement()
    {
        if (currentComponent != null)
        {
            Destroy(currentComponent); // ɾ��Ԫ��
            gridManager.isDragging = false;
            currentComponent = null;
        }
    }

}
