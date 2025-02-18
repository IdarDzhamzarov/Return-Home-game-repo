// Грязный код - нужно отрефакторить
using UnityEngine;

public class SpaceCapsule : MonoBehaviour
{
    [SerializeField] private CameraSystem camera;
    [SerializeField] private Transform spaceCapsule;

    [SerializeField] private Transform playerWhoEnteredTheSpaceCapsule;

    public bool isPlayerInTheSpaceCapsule;
    public void EnterSpaceCapsule()
    {
        playerWhoEnteredTheSpaceCapsule.gameObject.SetActive(false);
        isPlayerInTheSpaceCapsule = true;

        camera.cinemachineCamera.Target.TrackingTarget = spaceCapsule;
        camera.cinemachineCamera.Target.LookAtTarget = spaceCapsule;

        Debug.Log("Enter the space capsule!");
    }

    public void ExitSpaceCapsule()
    {
        playerWhoEnteredTheSpaceCapsule.gameObject.SetActive(true);
        isPlayerInTheSpaceCapsule = false;

        camera.cinemachineCamera.Target.TrackingTarget = playerWhoEnteredTheSpaceCapsule;
        camera.cinemachineCamera.Target.LookAtTarget = playerWhoEnteredTheSpaceCapsule;

        Debug.Log("Exit the space capsule");
    }
}
