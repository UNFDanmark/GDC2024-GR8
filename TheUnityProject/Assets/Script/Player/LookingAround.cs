using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAround : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private float sensitivity = 4;
    private Vector2 XYRotation;
    void Start()
    {
        cameraTransform = GetComponentInChildren<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        XYRotation.x -= mouseInput.y * sensitivity;
        XYRotation.y += mouseInput.x * sensitivity;
        XYRotation.x = Mathf.Clamp(XYRotation.x, -90, 90);

        cameraTransform.localEulerAngles = new Vector3(0f, XYRotation.y, 0f);
        transform.localEulerAngles = new Vector3(XYRotation.x, 0f, 0f);

    }
}
