using UnityEngine;
using static Models;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private DefaultInput defaultInput;
    public Vector2 inputMovement;
    public Vector2 inputView;
    private Vector3 newCameraRotation;
    private Vector3 newPlayerRotation;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -50;
    public float viewClampYMax = 80;

    [Header("Gravity")]
    public float gravityAmount;
    private float playerGravity;
    public float gravityMin;
    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;

    private void Awake() {
        defaultInput = new DefaultInput();
        defaultInput.Player.Movement.performed += e => inputMovement = e.ReadValue<Vector2>();
        defaultInput.Player.View.performed += e => inputView = e.ReadValue<Vector2>();
        defaultInput.Player.Jump.performed += e => Jump();
        defaultInput.Enable();
        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newPlayerRotation = transform.localRotation.eulerAngles;
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        CalculateView();
        CalculateMovement();
        CalculateJump();
    }

    private void CalculateView() {
        newPlayerRotation.y += playerSettings.viewXSensitivity * (playerSettings.viewXInverted ? -inputView.x : inputView.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newPlayerRotation);
        newCameraRotation.x += playerSettings.viewYSensitivity * (playerSettings.viewYInverted ? inputView.y : -inputView.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);
        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement() {
        float verticalSpeed = playerSettings.walkingForwardSpeed * inputMovement.y * Time.deltaTime;
        float horizontalSpeed = playerSettings.walkingStrafeSpeed * inputMovement.x * Time.deltaTime;
        Vector3 newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        if(playerGravity > gravityMin && jumpingForce.y < 0.1f) {
            playerGravity -= gravityAmount * Time.deltaTime;
        }
        
        if(playerGravity < -1 && characterController.isGrounded) {
            playerGravity = -1;
        }

        if(jumpingForce.y > 0.1f) {
            playerGravity = 0;
        }
        newMovementSpeed.y += playerGravity;
        newMovementSpeed += jumpingForce * Time.deltaTime;
        characterController.Move(newMovementSpeed);
    }

    private void CalculateJump() {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.jumpingFallof);
    }

    private void Jump() {
        if (!characterController.isGrounded) {
            return;
        }
        jumpingForce = Vector3.up * playerSettings.jumpingHeight;
    }
}
