using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCamera0530 : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] float cameraSensitivity;

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        xRotation = lookDelta.y * cameraSensitivity * Time.deltaTime;
        yRotation = lookDelta.x * cameraSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
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
