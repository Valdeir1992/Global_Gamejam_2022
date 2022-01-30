using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuSystem : MonoBehaviour
{
    private Camera _mainCamera;
    private float _timeElapsed;
    [SerializeField] private float _distance;
    [SerializeField] private Transform _center;
    [SerializeField] private float _velocity;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        _timeElapsed += Time.deltaTime * _velocity;
        _mainCamera.transform.position = _center.position + new Vector3(Mathf.Sin(_timeElapsed) * _distance, 
            _mainCamera.transform.position.y,
            Mathf.Cos(_timeElapsed) * _distance);
        _mainCamera.transform.LookAt(_center);
    }
}
