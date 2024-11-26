using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector3 WorldPositionOfGridOrigin; // 网格原点位置
    [SerializeField] private int gridWidth = 100; // 网格宽度
    [SerializeField] private int gridHeight = 100; // 网格高度
    [SerializeField] private float cellSize = 1.0f; // 单元格大小
    [SerializeField] private Camera mainCamera;
    public List<NewComponent> components;
    public List<NewWire> wires;
    public float CellSize { get { return cellSize; } }
    public bool isDragging=false;
    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("发现多个 GridManager 实例，已删除多余的实例。");
            Destroy(gameObject);
            return;
        }
        transform.position = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        WorldPositionOfGridOrigin = transform.position;
        components = new();
        wires = new();
    }

    private void Start()
    {
        // 可以选择性地在游戏开始时绘制网格线
        //DebugDrawLine();
    }

    private void Update()
    {
        // 可选：根据需要更新或重新绘制网格线
        //DebugDrawLine();
    }
    public void RemoveComponent(NewComponent component)
    {
        Debug.LogFormat("RemoveComponent");
        components.Remove(component);
    }
    public void PlaceComponent(NewComponent component)
    {
        components.Add(component);
    }
    public void RemoveWire(NewWire wire)
    {
        wires.Remove(wire);
    }
    public void PlaceWire(NewWire wire)
    {
        wires.Add(wire);
    }
    public bool CanBePlaced(NewWire wire)
    {
        foreach (var component in components)
        {
            if (component.PositionsOfBody.Intersect(wire.Positions).Any())
            {
                Debug.Log("conflict");
                return false;
            }
        }
        return true;
    }
    public bool CanBePlaced(NewComponent component)
    {
        foreach (var comp in components)
        {
            List<Vector2Int> positionOfAll = comp.PositionsOfPins.Concat(comp.PositionsOfBody).ToList();
            if (positionOfAll.Intersect(component.PositionsOfBody).Any() ||
                positionOfAll.Intersect(component.PositionsOfPins).Any() )
            {
                Debug.Log("conflict");
                return false;
            }
        }

        foreach (var wire in wires)
        {
            if (wire.Positions.Intersect(component.PositionsOfBody).Any())
                return false;
        }
        return true;
    }
    public bool CanBePlaced(NewComponent component,Vector2Int centerPosition)
    {
        List<Vector2Int> truePositionOfBody = new();
        List<Vector2Int> truePositionOfPin = new();
        foreach(var pos in component.Body.RelativePositions)
        {
            truePositionOfBody.Add(pos + centerPosition);
        }
        foreach (var pos in component.InputPins.RelativePositions)
        {
            truePositionOfPin.Add(pos + centerPosition);
        }
        foreach(var pos in component.OutputPins.RelativePositions)
        {
            truePositionOfPin.Add(pos + centerPosition);
        }
        foreach (var comp in components)
        {
            List<Vector2Int> positionOfAll = comp.PositionsOfPins.Concat(comp.PositionsOfBody).ToList();
            if(positionOfAll.Intersect(truePositionOfBody).Any() || positionOfAll.Intersect(truePositionOfPin).Any())
            {
                return false;
            }
        }
        foreach(var wire in wires)
        {
            if (wire.Positions.Intersect(truePositionOfBody).Any())
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * cellSize + WorldPositionOfGridOrigin.x,
                           gridPosition.y * cellSize + WorldPositionOfGridOrigin.y, 0);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - WorldPositionOfGridOrigin.x) / cellSize);
        int y = Mathf.FloorToInt((worldPosition.y - WorldPositionOfGridOrigin.y) / cellSize);
        return new Vector2Int(x, y);
    }

    public bool IsWithinGrid(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < gridWidth && gridPosition.y < gridHeight;
    }

    public bool IsWithinGrid(Vector3 worldPosition)
    {
        Vector2Int gridPosition = GetGridPosition(worldPosition);
        return IsWithinGrid(gridPosition);
    }

    public Vector3 SnapToGrid(Vector3 worldPosition)
    {
        return GetWorldPosition(GetGridPosition(worldPosition));
    }

    private void DebugDrawLine()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Vector3 worldPosition = GetWorldPosition(new Vector2Int(i, j));
                Debug.DrawLine(worldPosition, new Vector3(worldPosition.x + cellSize, worldPosition.y, 0), Color.green);
                Debug.DrawLine(worldPosition, new Vector3(worldPosition.x, worldPosition.y + cellSize, 0), Color.green);
            }
        }
    }
}
