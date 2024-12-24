using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class WireSaveData
{
    public Vector2Int StartPos;
    public Vector2Int TurningPos;
    public Vector2Int EndPos;
    public List<Vector2Int> OccupiedPos;

    public WireSaveData(Vector2Int startPos, Vector2Int turningPos, Vector2Int endPos, List<Vector2Int> occupiedPos)
    {
        StartPos = startPos;
        TurningPos = turningPos;
        EndPos = endPos;
        OccupiedPos = occupiedPos;
    }
}

