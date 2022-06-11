using UnityEngine;

public static class Utils
{
    private static Camera _mainCamera;

    public static Vector3 GetMouseWorldPosition()
    {
        if (!_mainCamera)
        {
            _mainCamera = Camera.main;
        }

        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        return mousePosition;
    }

}
