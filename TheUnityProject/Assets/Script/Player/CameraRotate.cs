using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    float mouseY;
    [SerializeField] float sensitivity = 4;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        //Debug.Log("First statement: " + !(transform.rotation.x <= -90) + "   Second statement: " + !(transform.rotation.x >= 90));
        //Debug.Log(transform.localEulerAngles);
        /*
        if ((transform.rotation.eulerAngles.x >= 270 && transform.rotation.eulerAngles.x <= 360) || (transform.rotation.eulerAngles.x <= 90 && transform.rotation.eulerAngles.x >= 0))
        {
            transform.Rotate(-mouseY,0,0);
        }
        */
        /*
        if (Vector3.Dot(transform.forward, new Vector3(0, 1, 0)) <= 0.8f &&
            Vector3.Dot(transform.forward, new Vector3(0, 1, 0)) >= -0.8f)
        {
            transform.Rotate(-mouseY,0,0);
        }
        */
        transform.Rotate(-mouseY,0,0);
        
        if (Vector3.Dot(transform.forward, new Vector3(0, 1, 0)) >= 0.95f || Vector3.Dot(transform.forward, new Vector3(0, 1, 0)) <= -0.95f)
        {
            transform.Rotate(mouseY,0,0);
        }
        
        //Debug.Log("First dot: " + Vector3.Dot(transform.forward, new Vector3(0, 1, 0)));
        //Debug.Log("Second dot: " + Vector3.Dot(transform.forward, new Vector3(1, 0, 0)));
    }
}
