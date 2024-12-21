using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 3.0f;
   [SerializeField] private float runSpeed = 5.0f;
   [SerializeField] private float turnSpeed = 4.0f;
   [SerializeField] private float gravity = 9.81f;

    private Vector3 moveDirection;
    private CharacterController characterController;
    private Animator animator;

    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isGrounded = characterController.isGrounded;

        if (!isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y = 0f;
        }

        if (canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            moveDirection.x = horizontal;
            moveDirection.z = vertical;

            bool isWalking = moveDirection.x != 0f || moveDirection.z != 0f;
            bool isRunning = Input.GetKey(KeyCode.LeftShift) && isWalking;

            if (isWalking)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }

            if (isRunning)
            {
                characterController.Move(moveDirection * runSpeed * Time.deltaTime);
            }
            else
            {
                characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
            }

            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isRunning", isRunning);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }
}
