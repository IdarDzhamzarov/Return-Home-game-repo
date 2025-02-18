using UnityEngine;
using Unity.Cinemachine;

public class CameraSystem : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;

    [SerializeField] private float targetFieldOfView = 20f;
    [SerializeField] private float fieldOfViewMax = 30f;
    [SerializeField] private float fieldOfViewMin = 10f;
    
    private void Update()
    {
        HandleCameraZoom();
    }
    public void HandleCameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= 5f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += 5f;
        }

        
        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

        float zoomSpeed = 10f;
        cinemachineCamera.Lens.FieldOfView =
            Mathf.Lerp(cinemachineCamera.Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
        
    }
}
