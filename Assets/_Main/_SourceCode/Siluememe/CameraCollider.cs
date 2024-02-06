using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _leftCollider;
    [SerializeField] private BoxCollider2D _topCollider;
    [SerializeField] private BoxCollider2D _rightCollider;
    [SerializeField] private BoxCollider2D _bottomCollider;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        if (!_camera.orthographic)
        {
            Debug.LogError("Make sure your camera is set to orthographic");
            return;
        }

        SetCameraEdges();
    }

    private void SetCameraEdges()
    {
        float halfHeight = _camera.orthographicSize;
        float halfWidth = _camera.orthographicSize * _camera.aspect;

        _leftCollider.offset = new Vector3(-halfWidth - _leftCollider.size.x * 0.5f, 0f, 10f);
        _leftCollider.size = new Vector3(_leftCollider.size.x, halfHeight * 2f, 20f);

        _topCollider.offset = new Vector3(0f, halfHeight + _topCollider.size.y * 0.5f, 10f);
        _topCollider.size = new Vector3(halfWidth * 2f + _topCollider.size.y * 2f, _topCollider.size.y, 20f);

        _rightCollider.offset = new Vector3(_leftCollider.offset.x * -1f, _leftCollider.offset.y);
        _rightCollider.size = _leftCollider.size;

        _bottomCollider.offset = new Vector3(_topCollider.offset.x, _topCollider.offset.y * -1f);
        _bottomCollider.size = _topCollider.size;
    }
}