using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private bool isRunning;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;
        isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);

        float rotateSpeed = 5f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        if (isRunning)
        {
            transform.position += moveDir * runSpeed * Time.deltaTime;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    public bool IsRunning()
    {
        return isRunning;
    }
}
