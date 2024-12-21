using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    public Transform camTransform;

    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float minDistance = 2.0f;
    [SerializeField] private float maxDistance = 10.0f; 
    [SerializeField] private float zoomSpeed = 4.0f;

    [SerializeField] private float currentCameraPositionX = 0.0f;
    [SerializeField] private float currentCameraPositionY = 50.0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        camTransform = transform;
    }

    private void Update()
    {
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentCameraPositionY, currentCameraPositionX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}

