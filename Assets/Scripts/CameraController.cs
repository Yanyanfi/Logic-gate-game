using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float zoomSpeed = 20f;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float maxZoom = 60f;
    private Camera mainCamera;
    private void HandlePan()
    {
        // ��ȡ����
        float horizontal = Input.GetAxis("Horizontal"); // A/D �� ��/�Ҽ�ͷ
        float vertical = Input.GetAxis("Vertical");     // W/S �� ��/�¼�ͷ

        // �����ƶ�����
        Vector3 direction = new (horizontal, vertical, 0f);
        // ƽ���ƶ������
        transform.position += mainCamera.orthographicSize * moveSpeed * Time.deltaTime * direction;
    }
    private void HandleZoom()
    {
        // ��ȡ����������
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            mainCamera.orthographicSize -= scroll * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
        }
    }
    void Update()
    {
        HandlePan();
        HandleZoom();
    }
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.aspect = 16f / 9;
        //transform.position = GridManager.Instance.transform.position;
    }
}
