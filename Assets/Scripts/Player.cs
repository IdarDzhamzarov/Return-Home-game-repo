using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        public SpaceCapsule selectedSpaceCapsule;
    }
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Light flashLight;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteraction;
    private SpaceCapsule selectedSpaceCapsule;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Player instance ");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnFlashLightEnabled += GameInput_OnFlashLightEnabled;

        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedSpaceCapsule == null)
        {
            Debug.Log("Nothing to interact");
        }

        if (selectedSpaceCapsule != null && selectedSpaceCapsule.isPlayerInTheSpaceCapsule == false)
        {
            selectedSpaceCapsule.EnterSpaceCapsule();
        }
        else
        {
            selectedSpaceCapsule.ExitSpaceCapsule();
        }
    }

    private void GameInput_OnFlashLightEnabled(object sender, EventArgs e)
    {
        flashLight.enabled = !flashLight.enabled;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteraction = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out SpaceCapsule spaceCapsule))
            {
                if(spaceCapsule != selectedSpaceCapsule)
                {
                    SetSelectedSpaceCapsule(spaceCapsule);
                }
               
            }
            else
            {
                SetSelectedSpaceCapsule(null);
            }            
        }
        else
        {
            SetSelectedSpaceCapsule(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 1f;
        float playerHeight = 1f;

        if (!CanMove(moveDir, moveDistance, playerRadius, playerHeight))
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;

            if (CanMove(moveDirX, moveDistance, playerRadius, playerHeight))
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                if (CanMove(moveDirZ, moveDistance, playerRadius, playerHeight))
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    return;
                }
            }
        }

        transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 5f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedSpaceCapsule(SpaceCapsule selectedSpaceCapsule)
    {
        this.selectedSpaceCapsule = selectedSpaceCapsule;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedSpaceCapsule = selectedSpaceCapsule
        });
    }

    private bool CanMove(Vector3 direction, float distance, float radius, float height)
    {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, direction, distance);
    }
}