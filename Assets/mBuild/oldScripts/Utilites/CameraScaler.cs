using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] private Vector2 DefaultResolution;
    [SerializeField] private float _defaultSize;

    private Camera _camera;
    private float _targetAspect;

    private void Update() => SetSize();

    private void OnValidate()
    {
        if (_camera == null)
            _camera = GetComponent<Camera>();

        SetSize();
    }

    private void SetSize()
    {
        _targetAspect = DefaultResolution.x / DefaultResolution.y;
        _camera.orthographicSize = _defaultSize * (_targetAspect / _camera.aspect);
    }
}
