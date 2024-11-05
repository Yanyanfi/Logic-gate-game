using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component:MonoBehaviour
{
    //元件除了引脚之外部分占用的坐标
    public List<Vector2Int> PositionOfBody{ get; set; }
    //元件引脚部分占用的坐标
    public List<Vector2Int> PositionOfInputPin { get; set; }
    public List<Vector2Int> PositionOfOutputPin { get; set; }

    //相对于元件中心的坐标
    public List<Vector2Int> RelativePositionOfBody { get; set; }
    public List<Vector2Int> RelativePositionOfInputPin { get; set; }
    public List<Vector2Int> RelativePositionOfOutputPin { get; set; }
    public virtual void SetPosition(Vector2Int pos)
    {

    }
}
