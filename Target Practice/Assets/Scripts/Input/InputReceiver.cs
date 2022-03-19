using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    public static InputReceiver instance;

    PlayerInput inputActions;

    public Vector2 moveInput { get; private set; }
    public Vector2 cameraInput { get; private set; }

    public System.Action OnFirePressed;
    public System.Action OnJumpPressed;
    public System.Action OnCrouchPressed;
    public System.Action OnCrouchReleased;
    public System.Action OnAimPressed;
    public System.Action OnAimReleased;

    private void Awake()
    {
        instance = this;
        inputActions = new PlayerInput();

        inputActions.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Gameplay.Move.canceled += _ => moveInput = Vector2.zero;

        inputActions.Gameplay.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        inputActions.Gameplay.Camera.canceled += _ => cameraInput = Vector2.zero;

        inputActions.Gameplay.Shoot.performed += _ => OnFirePressed?.Invoke();
        inputActions.Gameplay.Jump.performed += _ => OnJumpPressed?.Invoke();

        inputActions.Gameplay.Crouch.performed += _ => OnCrouchPressed?.Invoke();
        inputActions.Gameplay.Crouch.canceled += _ => OnCrouchReleased?.Invoke();

        inputActions.Gameplay.Aim.performed += _ => OnAimPressed?.Invoke();
        inputActions.Gameplay.Aim.canceled += _ => OnAimReleased?.Invoke();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
