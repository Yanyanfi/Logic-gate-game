using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class Body
{
    public List<Vector2Int> RelativePositions { get; } = new();
    public void AddRelativePosition(int posX,int posY)
    {
        RelativePositions.Add(new Vector2Int(posX,posY));
    }
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

