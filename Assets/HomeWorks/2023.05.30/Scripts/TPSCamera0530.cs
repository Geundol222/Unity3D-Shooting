using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCamera0530 : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float lookDistance;

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Rotate()
    {
        Vector3 lookDir = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;

        lookDir.y = transform.position.y;

        transform.LookAt(lookDir);
    }

    private void Look()
    {
        yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;
        xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
