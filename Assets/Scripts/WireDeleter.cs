using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireDeleter : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    private Wire wire;
    private void Awake()
    {
        wire = GetComponent<Wire>();
        gridManager = FindObjectOfType<GridManager>();
        mainCamera = FindObjectOfType<Camera>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2Int CursorGridPosition = gridManager.GetGridPosition(mainCamera.ScreenToWorldPoint(Input.mousePosition));
            if (wire.Position.Contains(CursorGridPosition)) 
            {
                gridManager.RemoveWire(wire);
                Destroy(gameObject);
            }
        }
    }
}
