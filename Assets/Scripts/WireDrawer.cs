using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using System.Net;
using Unity.VisualScripting;

public class WireDrawer : MonoBehaviour
{
    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private float width = 0.8f;
    private GameObject tempWire;
    private NewWire wire;
    private Vector2Int startPos;
    private Vector2Int endPos;
    private Vector2Int turningPos;
    private bool isDrawing;
    public static WireDrawer Instance;
    
    //根据起点和终点返回转折点
    private Vector2Int GetTurningPos(Vector2Int startPos,Vector2Int endPos)
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

    //返回线路占据的坐标列表
    private List<Vector2Int> GetOccupiedPos(Vector2Int startPos, Vector2Int turningPos, Vector2Int endPos)
    {
        List<Vector2Int> list = new();
        list.AddRange(GetLineCoordinates(startPos, turningPos, true));
        list.AddRange(GetLineCoordinates(turningPos, endPos, false));
        return list;
    }
    //返回两点之间的连线的坐标列表，仅限水平竖直或斜率为1的线，不符合条件的返回空列表；abandonEnd==true抛弃终点（左闭右开）
    private List<Vector2Int> GetLineCoordinates(Vector2Int start,Vector2Int end,bool abandonEnd)
    {
        List<Vector2Int> list = new();
        int x = start.x;
        int y = start.y;
        int dx = Mathf.Abs(start.x - end.x);
        int dy = Mathf.Abs(start.y - end.y);
        int sx = 0;
        int sy = 0;
        if (start.x < end.x)
            sx = 1;
        else if (start.x > end.x)
            sx = -1;
        if (start.y < end.y)
            sy = 1;
        else if (start.y > end.y)
            sy = -1;
        if (dx == 0 || dy == 0 || dx == dy)
        {
            while (true)
            {
                list.Add(new Vector2Int(x,y));
                if (x == end.x&&y==end.y)
                    break;
                x += sx;
                y += sy;
            }
        }
        if (abandonEnd && list.Count > 0)
            list.RemoveAt(list.Count - 1);
        return list;
    }
    private void DrawLine(LineRenderer lineRenderer,Vector2Int startPos,Vector2Int turningPos,Vector2Int endPos)
    {
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        Vector3 fixPos = new (gridManager.CellSize / 2, gridManager.CellSize / 2, 0);
        Vector3 worldStartPos = gridManager.GetWorldPosition(startPos)+ fixPos;
        Vector3 worldTurningPos= gridManager.GetWorldPosition(turningPos) + fixPos;
        Vector3 worldEndPos= gridManager.GetWorldPosition(endPos)+fixPos;
        Vector3[] points = new Vector3[] { worldStartPos, worldTurningPos, worldEndPos };
        lineRenderer.positionCount = points.Length;
        
        lineRenderer.startWidth = gridManager.CellSize * width;
        lineRenderer.endWidth = gridManager.CellSize* width;
        lineRenderer.SetPositions(points);
        Debug.LogFormat($"start:{startPos},turning:{turningPos},end:{endPos}");
        Debug.Log($"Start Color: {lineRenderer.startColor}, End Color: {lineRenderer.endColor}");

    }
    public void DrawWire(NewWire wire, Vector2Int startPos, Vector2Int turningPos, Vector2Int endPos)
    {
        DrawLine(wire.GetComponent<LineRenderer>(), startPos, turningPos, endPos);
        wire.Positions = GetOccupiedPos(startPos, turningPos, endPos);
        wire.StartPosition = startPos;
        wire.TurningPosition = turningPos;
        wire.EndPosition = endPos;
    }
    //private void AddCollider(Vector2Int startPos,Vector2Int turningPos,Vector2Int endPos)
    //{
    //    GameObject colliderObject1 = new GameObject();
    //    GameObject colliderObject2 = new GameObject();
    //    colliderObject1.transform.SetParent(tempWire.transform);
    //    colliderObject2.transform.SetParent(tempWire.transform);
    //    Vector3 fixPos = new Vector3(gridManager.CellSize / 2, gridManager.CellSize / 2,0);
    //    Vector3 fixedWorldStartPos = gridManager.GetWorldPosition(startPos)+fixPos;
    //    Vector3 fixedWorldTurningPos = gridManager.GetWorldPosition(turningPos)+fixPos;
    //    Vector3 fixedWorldEndPos = gridManager.GetWorldPosition(endPos)+fixPos;
    //    BoxCollider2D collider1 = colliderObject1.AddComponent<BoxCollider2D>();
    //    BoxCollider2D collider2 = colliderObject2.AddComponent<BoxCollider2D>();
    //    float length1 = Vector3.Distance(fixedWorldStartPos, fixedWorldTurningPos);
    //    float length2 = Vector3.Distance(fixedWorldTurningPos, fixedWorldEndPos);
    //    float angle1 = Mathf.Atan2(fixedWorldTurningPos.y - fixedWorldStartPos.y, fixedWorldTurningPos.x - fixedWorldStartPos.x) * Mathf.Rad2Deg;
    //    float angle2 = Mathf.Atan2(fixedWorldEndPos.y - fixedWorldTurningPos.y, fixedWorldEndPos.x - fixedWorldTurningPos.x) * Mathf.Rad2Deg;
    //    collider1.size = new Vector2(length1, gridManager.CellSize);
    //    collider2.size = new Vector2(length2, gridManager.CellSize);
    //    colliderObject1.transform.position = transform.InverseTransformPoint((fixedWorldTurningPos + fixedWorldStartPos) / 2) ;
    //    colliderObject2.transform.position = transform.InverseTransformPoint((fixedWorldEndPos + fixedWorldTurningPos) / 2);
    //    colliderObject1.transform.rotation = Quaternion.Euler(0, 0, angle1);
    //    colliderObject2.transform.rotation = Quaternion.Euler(0, 0, angle2);
    //}
    private void Update()
    {
        if (gridManager.isDragging == false)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    isDrawing = true;
                    startPos= gridManager.GetGridPosition(worldPos);
                    tempWire = Instantiate(wirePrefab);
                    wire = tempWire.GetComponent<NewWire>();
                } 
            }
        }
        if (isDrawing)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                endPos = gridManager.GetGridPosition(worldPos);
                turningPos = GetTurningPos(startPos, endPos);
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    DrawWire(wire, startPos, turningPos, endPos);
                }
                
            }
            if (Input.GetMouseButtonUp(0)&&isDrawing)
            {
                isDrawing = false;
                if (startPos == endPos || gridManager.CanBePlaced(wire)==false)
                {
                    Destroy(tempWire);
                }
                else
                {
                    gridManager.PlaceWire(wire);
                }
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        isDrawing = false;
    }

}
