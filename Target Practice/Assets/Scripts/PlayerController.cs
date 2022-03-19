using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform mainCamRotator;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float maxCameraAngle = 70f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityMultiplier = 1f;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform crosshair;
    [SerializeField] Transform shootPoint;
    [SerializeField] Rigidbody bulletPrefab;

    CharacterController controller;
    Vector3 playerVelocity;
    float normalJumpHeight;
    float crosshairSize;
    bool checkForStopCrouch;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        InputReceiver.instance.OnJumpPressed += Jump;
        InputReceiver.instance.OnCrouchPressed += () => SetCrouch(true);
        InputReceiver.instance.OnCrouchReleased += () => SetCrouch(false);
        InputReceiver.instance.OnAimPressed += () => SetAiming(true);
        InputReceiver.instance.OnAimReleased += () => SetAiming(false);
        InputReceiver.instance.OnFirePressed += Shoot;

        normalJumpHeight = jumpHeight;
        crosshairSize = crosshair.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerRotation();
        HandleCameraRotation();
        HandleCrosshairSize();
        CrouchCheck();
    }

    private void CrouchCheck()
    {
        if (!checkForStopCrouch) return;
        if (!Physics.Raycast(transform.position, Vector3.up, 1.2f, ground))
        {
            SetCrouch(false);
            checkForStopCrouch = false;
        }
    }

    private void HandleCrosshairSize()
    {
        if (!controller.isGrounded)
        {
            crosshair.transform.localScale = Vector3.MoveTowards(crosshair.transform.localScale, crosshairSize * 2 * Vector3.one, 2f * Time.deltaTime);
        }
        else
        {
            crosshair.transform.localScale = Vector3.MoveTowards(crosshair.transform.localScale, crosshairSize * Vector3.one, 2f * Time.deltaTime);
        }
    }

    private void HandleCameraRotation()
    {
        Vector3 euler = mainCamRotator.transform.eulerAngles;
        euler.x -= InputReceiver.instance.cameraInput.y * rotateSpeed * Time.deltaTime;
        if (euler.x > 180)
        {
            euler.x -= 360f;
        }
        euler.x = Mathf.Clamp(euler.x, -maxCameraAngle, maxCameraAngle);
        mainCamRotator.transform.rotation = Quaternion.Euler(euler);
    }

    private void HandlePlayerRotation()
    {
        Vector3 euler = transform.eulerAngles;
        euler.y += InputReceiver.instance.cameraInput.x * rotateSpeed  * Time.deltaTime;
        transform.rotation = Quaternion.Euler(euler);
    }

    private void FixedUpdate()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        Vector3 move = new Vector3(InputReceiver.instance.moveInput.x, 0, InputReceiver.instance.moveInput.y);
        move = Vector3.ClampMagnitude(move, 1f);
        controller.Move(transform.TransformDirection(move) * Time.fixedDeltaTime * moveSpeed);

        if (Physics.Raycast(transform.position, Vector3.up, 1.1f, ground))
        {
            playerVelocity.y = 0;
        }
        playerVelocity.y += gravityMultiplier * Physics.gravity.y * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityMultiplier * Physics.gravity.y);
        }
    }

    void SetCrouch(bool value)
    {
        if (value)
        {
            jumpHeight = 0;
            moveSpeed /= 3f;
            controller.height /= 2f;
        }
        else if (!Physics.Raycast(transform.position, Vector3.up, 1.2f, ground))
        {
            jumpHeight = normalJumpHeight;
            moveSpeed *= 3f;
            controller.height *= 2f;
        }
        else
        {
            checkForStopCrouch = true;
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Ray checkRay = new Ray(mainCamRotator.transform.position, mainCamRotator.transform.forward);
        Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        if (Physics.Raycast(checkRay, out hit, Mathf.Infinity, ground))
        {
            //Shoot the bullet
            Vector3 shootOffset = hit.point - shootPoint.position;
            bullet.velocity = 75f * shootOffset.normalized;
        }
        else
        {
            bullet.velocity = 75f * shootPoint.forward;
        }
    }

    void SetAiming(bool value)
    {
        if (value)
        {
            moveSpeed /= 2f;
            jumpHeight = 0f;
        }
        else
        {
            moveSpeed *= 2f;
            jumpHeight = normalJumpHeight;
        }
    }
}
