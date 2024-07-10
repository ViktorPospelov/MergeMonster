using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Vector2 DefaultResolution;

    private Camera _camera;
    private float _defaultSize;
    private float _targetAspect;

    private void Update() => SetSize();

    private void OnValidate()
    {
        if (_camera == null)
        {
            _camera = Camera.main;

            _defaultSize = _camera.orthographicSize;
            _targetAspect = DefaultResolution.x / DefaultResolution.y;

            _camera.orthographicSize = _defaultSize * (_targetAspect / _camera.aspect);
        }

        SetSize();
    }

    private void SetSize()
    {
        float constantWidthSize = _defaultSize * (_targetAspect / _camera.aspect);
        _camera.orthographicSize = constantWidthSize;
    }
}
