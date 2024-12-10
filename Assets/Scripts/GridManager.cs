using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// 网格管理器类，在场景中存在且仅存在唯一的实例。<br/>
/// 所有和放置元件、移动元件、绘制线路以及编译电路相关的脚本都需要引用访问这个实例。
/// <para>注意：管理器存放的元件指的是游戏对象的<see cref="NewComponent"/>组件</para>
/// </summary>
public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector3 WorldPositionOfGridOrigin; // 网格原点位置
    [SerializeField] private int gridWidth = 100; // 网格宽度
    [SerializeField] private int gridHeight = 100; // 网格高度
    [SerializeField] private float cellSize = 1.0f; // 单元格大小
    [SerializeField] private Camera mainCamera;
    public List<NewComponent> components=new();
    public List<NewWire> wires=new();
    public float CellSize => cellSize;
    public bool isDragging=false;//
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
        InitGrid();
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
    private void InitGrid()
    {
        List<NewComponent> staticComponents = FindObjectsOfType<NewComponent>().ToList();
        foreach(var component in staticComponents)
        {
            Vector2Int gridPos = GetGridPosition(component.transform.position);
            component.SetPositions(gridPos);
            component.transform.position = SnapToGrid(gridPos);
            components.Add(component);
        }
    }

    /// <summary>
    /// 将元件从<see cref="components"/>中移除
    /// </summary>
    /// <param name="component">要移除的元件</param>
    public void RemoveComponent(NewComponent component)
    {
        Debug.LogFormat("RemoveComponent");
        components.Remove(component);
    }

    /// <summary>
    /// 将元件添加到<see cref="components"/>中;
    /// <para>
    /// 使用该方法前需要完成下面的工作:<br/>
    /// 1.元件调用自身的方法<see cref="NewComponent.SetPositions(Vector2Int)"/>设置了自己在网格中位置属性：<br/>
    /// <see cref="NewComponent.PositionsOfBody"/>和<see cref="NewComponent.PositionsOfPins"/>;<br/>
    /// 2.使用<see cref="CanBePlaced(NewComponent)"/> 或 <see cref="CanBePlaced(NewComponent, Vector2Int)"/><br/>
    /// 方法检查元件在网格中是否与其他元件或线的位置重合。如果返回<see langword="true"/>则表明元件可放置;
    /// </para>
    /// </summary>
    /// <param name="component">被放置的元件</param>
    public void PlaceComponent(NewComponent component)
    {
        components.Add(component);
    }

    /// <summary>
    /// 将线路从<see cref="wires"/>中移除
    /// </summary>
    /// <param name="wire">需要移除的线路</param>
    public void RemoveWire(NewWire wire)
    {
        wires.Remove(wire);
    }

    /// <summary>
    /// 将线路添加到<see cref="wires"/>中;
    /// <para>
    /// 在使用该方法前需要完成以下工作：<br/>
    /// 1.wire的坐标列表<see cref="NewWire.Positions"/>已被正确设置;<br/>
    /// 2.使用<see cref="CanBePlaced(NewWire)"/>方法检查 wire 是否可以放置
    /// </para>
    /// </summary>
    /// <param name="wire"></param>
    public void PlaceWire(NewWire wire)
    {
        wires.Add(wire);
    }

    /// <summary>
    /// 检查线路是否可以被放置；<br/>
    /// 需要<see cref="NewWire.Positions"/>已经被设置
    /// </summary>
    /// <param name="wire">被检查的线路</param>
    /// <returns></returns>
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

    /// <summary>
    /// 检查元件是否可以被放置；<br/>
    /// 元件需要在调用自己的方法<see cref="NewComponent.SetPositions(Vector2Int)"/>后才可以使用此检测方法
    /// </summary>
    /// <param name="component">被检查的元件</param>
    /// <returns></returns>
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

    /// <summary>
    /// 检查元件是否可以被放置
    /// </summary>
    /// <param name="component">被检查的元件</param>
    /// <param name="centerPosition">当前元件的中心在网格中的坐标</param>
    /// <returns></returns>
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

    /// <summary>
    /// 将网格坐标转换成世界坐标
    /// </summary>
    /// <param name="gridPosition">网格坐标</param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * cellSize + WorldPositionOfGridOrigin.x,
                           gridPosition.y * cellSize + WorldPositionOfGridOrigin.y, 0);
    }

    /// <summary>
    /// 将世界坐标转换成网格坐标
    /// </summary>
    /// <param name="worldPosition">世界坐标</param>
    /// <returns></returns>
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - WorldPositionOfGridOrigin.x) / cellSize);
        int y = Mathf.FloorToInt((worldPosition.y - WorldPositionOfGridOrigin.y) / cellSize);
        return new Vector2Int(x, y);
    }

    //暂不使用(可以认为网格无限大)
    /// <summary>
    /// 检查对象有没有超出边界
    /// </summary>
    /// <param name="gridPosition">网格坐标</param>
    /// <returns></returns>
    public bool IsWithinGrid(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < gridWidth && gridPosition.y < gridHeight;
    }

    //暂不使用(可以认为网格无限大)
    /// <summary>
    /// 检查对象有没有超出边界
    /// </summary>
    /// <param name="worldPosition">世界坐标</param>
    /// <returns></returns>
    public bool IsWithinGrid(Vector3 worldPosition)
    {
        Vector2Int gridPosition = GetGridPosition(worldPosition);
        return IsWithinGrid(gridPosition);
    }
    /// <summary>
    /// 将世界坐标转换成所在格子中心的世界坐标
    /// </summary>
    /// <param name="worldPosition">世界坐标</param>
    /// <returns></returns>
    public Vector3 SnapToGrid(Vector3 worldPosition)
    {
        return GetWorldPosition(GetGridPosition(worldPosition)) + new Vector3(CellSize / 2, CellSize / 2, 0f);
    }

    /// <summary>
    /// 将所在格子的网格坐标转换成格子中心的世界坐标
    /// </summary>
    /// <param name="gridPosition">网格坐标</param>
    /// <returns></returns>
    public Vector3 SnapToGrid(Vector2Int gridPosition)
    {
        return  GetWorldPosition(gridPosition) + new Vector3(CellSize / 2, CellSize / 2, 0f);
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
