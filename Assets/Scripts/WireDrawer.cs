using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class WireDrawer : MonoBehaviour
{
    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GridManager gridManager;
    private GameObject tempWire;
    private Vector2Int prePos;
    private Vector2Int startPos;
    private Vector2Int endPos;
    private bool isDrawing;
    

    private Vector2Int getTurningPos(Vector2Int startPos,Vector2Int endPos)
    {
        Vector2Int turningPos=new();
        if(Mathf.Abs(startPos.x-endPos.x)>=Mathf.Abs(startPos.y - endPos.y))
        {
            turningPos.y = startPos.y;
            if (startPos.x < endPos.x)
            {
                turningPos.x = endPos.x - Mathf.Abs(endPos.y - startPos.y);
            }
            else
            {
                turningPos.x = endPos.x +Mathf.Abs(endPos.y - startPos.y);
            }
        }
        else
        {
            turningPos.x = startPos.x;
            if (startPos.y < endPos.y)
            {
                turningPos.y = endPos.y - Mathf.Abs(endPos.x - startPos.x);
            }
            else
            {
                turningPos.y = endPos.y + Mathf.Abs(endPos.x - startPos.x);
            }
        }
        return turningPos;
    }
    private void DrawLine(LineRenderer lineRenderer,Vector2Int startPos,Vector2Int turningPos,Vector2Int endPos)
    {
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        Vector3 fixPos = new Vector3(gridManager.CellSize / 2, gridManager.CellSize / 2, 0);
        Vector3 worldStartPos = gridManager.GetWorldPosition(startPos)+ fixPos;
        Vector3 worldTurningPos= gridManager.GetWorldPosition(turningPos) + fixPos;
        Vector3 worldEndPos= gridManager.GetWorldPosition(endPos)+fixPos;
        Vector3[] points = new Vector3[] { worldStartPos, worldTurningPos, worldEndPos };
        lineRenderer.positionCount = points.Length;
        
        lineRenderer.startWidth = gridManager.CellSize;
        lineRenderer.endWidth = gridManager.CellSize;
        lineRenderer.SetPositions(points);
        Debug.LogFormat($"start:{startPos},turning:{turningPos},end:{endPos}");
        Debug.Log($"Start Color: {lineRenderer.startColor}, End Color: {lineRenderer.endColor}");

    }
    private void DeleteLine(LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = 0;
    }
    private void Update()
    {
        if (gridManager.isDragging == false)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false && Physics2D.Raycast(worldPos, Vector2.zero).collider == null)
                {
                    isDrawing = true;
                    startPos= gridManager.GetGridPosition(worldPos);
                    tempWire = Instantiate(wirePrefab);
                } 
            }
        }
        if (isDrawing)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int gridPos = gridManager.GetGridPosition(worldPos);
                if(EventSystem.current.IsPointerOverGameObject() == false && Physics2D.Raycast(worldPos, Vector2.zero).collider == null)
                {
                    Vector2Int turningPos = getTurningPos(startPos, gridPos);
                    DrawLine(tempWire.GetComponent<LineRenderer>(),startPos, turningPos,gridPos);
                }
                
            }
            if (Input.GetMouseButtonUp(0)&&isDrawing)
            {
                isDrawing = false;

            }
        }
    }

    private void Awake()
    {
        isDrawing = false;
    }

}
