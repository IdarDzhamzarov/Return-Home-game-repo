using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnFlashLightEnabled;
    public event EventHandler OnEscapeButtonPressed;
    
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.UI.Enable();

        playerInputActions.Player.Flashlight.performed += Flashlight_performed;

        playerInputActions.Player.Interact.performed += Interact_performed;

        playerInputActions.UI.EscapeMenu.performed += EscapeMenu_performed;

        
    }

    private void EscapeMenu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEscapeButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Flashlight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFlashLightEnabled?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;

        return inputVector;
    }
}
