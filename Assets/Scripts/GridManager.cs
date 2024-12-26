using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// ����������࣬�ڳ����д����ҽ�����Ψһ��ʵ����<br/>
/// ���кͷ���Ԫ�����ƶ�Ԫ����������·�Լ������·��صĽű�����Ҫ���÷������ʵ����
/// <para>ע�⣺��������ŵ�Ԫ��ָ������Ϸ�����<see cref="NewComponent"/>���</para>
/// </summary>
public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector3 WorldPositionOfGridOrigin; // ����ԭ��λ��
    [SerializeField] private int gridWidth = 100; // ������
    [SerializeField] private int gridHeight = 100; // ����߶�
    [SerializeField] private float cellSize = 1.0f; // ��Ԫ���С
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
            Debug.LogWarning("���ֶ�� GridManager ʵ������ɾ�������ʵ����");
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
        // ����ѡ���Ե�����Ϸ��ʼʱ����������
        //DebugDrawLine();
    }

    private void Update()
    {
        // ��ѡ��������Ҫ���»����»���������
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
    /// ��Ԫ����<see cref="components"/>���Ƴ�
    /// </summary>
    /// <param name="component">Ҫ�Ƴ���Ԫ��</param>
    public void RemoveComponent(NewComponent component)
    {
        Debug.LogFormat("RemoveComponent");
        components.Remove(component);
    }

    /// <summary>
    /// ��Ԫ����ӵ�<see cref="components"/>��;
    /// <para>
    /// ʹ�ø÷���ǰ��Ҫ�������Ĺ���:<br/>
    /// 1.Ԫ����������ķ���<see cref="NewComponent.SetPositions(Vector2Int)"/>�������Լ���������λ�����ԣ�<br/>
    /// <see cref="NewComponent.PositionsOfBody"/>��<see cref="NewComponent.PositionsOfPins"/>;<br/>
    /// 2.ʹ��<see cref="CanBePlaced(NewComponent)"/> �� <see cref="CanBePlaced(NewComponent, Vector2Int)"/><br/>
    /// �������Ԫ�����������Ƿ�������Ԫ�����ߵ�λ���غϡ��������<see langword="true"/>�����Ԫ���ɷ���;
    /// </para>
    /// </summary>
    /// <param name="component">�����õ�Ԫ��</param>
    public void PlaceComponent(NewComponent component)
    {
        components.Add(component);
    }

    /// <summary>
    /// ����·��<see cref="wires"/>���Ƴ�
    /// </summary>
    /// <param name="wire">��Ҫ�Ƴ�����·</param>
    public void RemoveWire(NewWire wire)
    {
        wires.Remove(wire);
    }

    /// <summary>
    /// ����·��ӵ�<see cref="wires"/>��;
    /// <para>
    /// ��ʹ�ø÷���ǰ��Ҫ������¹�����<br/>
    /// 1.wire�������б�<see cref="NewWire.Positions"/>�ѱ���ȷ����;<br/>
    /// 2.ʹ��<see cref="CanBePlaced(NewWire)"/>������� wire �Ƿ���Է���
    /// </para>
    /// </summary>
    /// <param name="wire"></param>
    public void PlaceWire(NewWire wire)
    {
        wires.Add(wire);
    }

    /// <summary>
    /// �����·�Ƿ���Ա����ã�<br/>
    /// ��Ҫ<see cref="NewWire.Positions"/>�Ѿ�������
    /// </summary>
    /// <param name="wire">��������·</param>
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
    /// ���Ԫ���Ƿ���Ա����ã�<br/>
    /// Ԫ����Ҫ�ڵ����Լ��ķ���<see cref="NewComponent.SetPositions(Vector2Int)"/>��ſ���ʹ�ô˼�ⷽ��
    /// </summary>
    /// <param name="component">������Ԫ��</param>
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
    /// ���Ԫ���Ƿ���Ա�����
    /// </summary>
    /// <param name="component">������Ԫ��</param>
    /// <param name="centerPosition">��ǰԪ���������������е�����</param>
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
    /// ����������ת������������
    /// </summary>
    /// <param name="gridPosition">��������</param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * cellSize + WorldPositionOfGridOrigin.x,
                           gridPosition.y * cellSize + WorldPositionOfGridOrigin.y, 0);
    }

    /// <summary>
    /// ����������ת������������
    /// </summary>
    /// <param name="worldPosition">��������</param>
    /// <returns></returns>
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x - WorldPositionOfGridOrigin.x) / cellSize);
        int y = Mathf.FloorToInt((worldPosition.y - WorldPositionOfGridOrigin.y) / cellSize);
        return new Vector2Int(x, y);
    }

    //�ݲ�ʹ��(������Ϊ�������޴�)
    /// <summary>
    /// ��������û�г����߽�
    /// </summary>
    /// <param name="gridPosition">��������</param>
    /// <returns></returns>
    public bool IsWithinGrid(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < gridWidth && gridPosition.y < gridHeight;
    }

    //�ݲ�ʹ��(������Ϊ�������޴�)
    /// <summary>
    /// ��������û�г����߽�
    /// </summary>
    /// <param name="worldPosition">��������</param>
    /// <returns></returns>
    public bool IsWithinGrid(Vector3 worldPosition)
    {
        Vector2Int gridPosition = GetGridPosition(worldPosition);
        return IsWithinGrid(gridPosition);
    }
    /// <summary>
    /// ����������ת�������ڸ������ĵ���������
    /// </summary>
    /// <param name="worldPosition">��������</param>
    /// <returns></returns>
    public Vector3 SnapToGrid(Vector3 worldPosition)
    {
        return GetWorldPosition(GetGridPosition(worldPosition)) + new Vector3(CellSize / 2, CellSize / 2, 0f);
    }

    /// <summary>
    /// �����ڸ��ӵ���������ת���ɸ������ĵ���������
    /// </summary>
    /// <param name="gridPosition">��������</param>
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
