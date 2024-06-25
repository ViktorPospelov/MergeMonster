using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Vector2 DefaultResolution;

    private Camera _camera;
    private float _defaultSize;
    private float _targetAspect;

    private void Start()
    {
        _camera = Camera.main;

        _defaultSize = _camera.orthographicSize;
        _targetAspect = DefaultResolution.x / DefaultResolution.y;
    }

    private void Update()
    {
        float constantWidthSize = _defaultSize * (_targetAspect / _camera.aspect);
        _camera.orthographicSize = constantWidthSize;
    }
}
