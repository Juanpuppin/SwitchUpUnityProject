/// <summary>
/// Tracks the avatar position and orbits arround it
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float zoomSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private float _yAngleLimit = 45f;

    private float _currentDistance;
    private float _desiredDistance;
    [SerializeField] private float _xRotation = 0.0f;
    [SerializeField] private float _yRotation = 0.0f;


    private void Start()
    {
        _currentDistance = distance;
        _desiredDistance = distance;
    }

    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.InGame)
        {
            GetInput();
            Zoom();
            MoveCamera();
        } 
    }


    private void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            RotateCamera();
        }
    }

    private void RotateCamera() 
    {     
        _xRotation += (Input.GetAxis("Mouse X")) * rotationSpeed;      
        _yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
        _yRotation = Mathf.Clamp(_yRotation, 15, _yAngleLimit);
    }

    private void Zoom()
    {
        _desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        _desiredDistance = Mathf.Clamp(_desiredDistance, 2.0f, 15.0f);
        _currentDistance = Mathf.Lerp(_currentDistance, _desiredDistance, Time.deltaTime * 10.0f);
    }

    private void MoveCamera()
    {
        Quaternion rotation = Quaternion.Euler(_yRotation, _xRotation, 0);
        Vector3 offset = new Vector3(0, 0, -_currentDistance);
        Vector3 newPosition = (target.position) + rotation * offset;

        transform.position = newPosition;
        transform.LookAt(target);
    }
}
