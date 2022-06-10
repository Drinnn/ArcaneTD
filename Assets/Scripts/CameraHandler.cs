using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomAmount = 2f;
    [SerializeField] private float minOrtographicSize = 10f;
    [SerializeField] private float maxOrtographicSize = 30f;

    private float _ortographicSize;
    private float _targetOrtographicSize;

    private void Start()
    {
        _ortographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        _targetOrtographicSize = _ortographicSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, y).normalized;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        _targetOrtographicSize -= Input.mouseScrollDelta.y * zoomAmount;
        _targetOrtographicSize = Mathf.Clamp(_targetOrtographicSize, minOrtographicSize, maxOrtographicSize);
        _ortographicSize = Mathf.Lerp(_ortographicSize, _targetOrtographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = _ortographicSize;
    }
}
