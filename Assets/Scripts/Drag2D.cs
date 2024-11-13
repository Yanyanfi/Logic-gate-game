using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drag2D : MonoBehaviour
{
    private Camera mainCamera;
    private Component component;
     
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1)&&GridManager.Instance.isDragging==false)
        {
            DestroyComponent();

            GridManager.Instance.isDragging = false;
        }
    }

    //private void OnMouseDown()
    //{
    //    //…Í«ÎÕœ∂Ø
    //    if (!GridManager.Instance.isDragging)
    //    {
    //        GridManager.Instance.isDragging = true;
    //        GridManager.Instance.RemoveComponent(component);
    //    }
    //}
    //private void OnMouseDrag()
    //{
    //    if (GridManager.Instance.isDragging)
    //    {
    //        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    //        Vector2Int gridPosition = GridManager.Instance.GetGridPosition(worldPosition);
    //        //component.SetPosition(GridManager.Instance.GetGridPosition(worldPosition));
    //        if (GridManager.Instance.CanBePlaced(component, gridPosition))
    //        {
    //            transform.position = GridManager.Instance.SnapToGrid(worldPosition) + new Vector3(0.5f, 0.5f, 0);
    //        }
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90);
    //        }
    //    }
    //}
    //private void OnMouseUp()
    //{
    //    Vector2Int gridPosition = GridManager.Instance.GetGridPosition(transform.position);
    //    component.SetPosition(gridPosition);
    //    GridManager.Instance.PlaceComponent(component);
    //    GridManager.Instance.isDragging = false;
    //}
    void Awake()
    {
        component = gameObject.GetComponent<Component>();
        mainCamera = FindObjectOfType<Camera>();
    }

    private void DestroyComponent()
    {
        GridManager.Instance.RemoveComponent(component);
        Destroy(gameObject);
    }
}
