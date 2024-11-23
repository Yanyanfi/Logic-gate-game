using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 这个类管理元件除了引脚之外的身体部分,
/// 仅包含身体部分的坐标
/// </summary>
public class Body
{
    public List<Vector2Int> RelativePositions { get; } = new();
    /// <summary>
    /// 添加身体部分相对于游戏对象中心（坐标(0,0)）的坐标
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    public void AddRelativePosition(int posX,int posY)
    {
        RelativePositions.Add(new Vector2Int(posX,posY));
    }
    /// <summary>
    /// 顺时针旋转身体部分的坐标
    /// </summary>
    public void Rotate()
    { 
        for (int i = 0; i < RelativePositions.Count; i++)
        {
            int x = RelativePositions[i].x;
            int y = RelativePositions[i].y;
            RelativePositions[i] = new Vector2Int(y, -x);
        }
    }
}

