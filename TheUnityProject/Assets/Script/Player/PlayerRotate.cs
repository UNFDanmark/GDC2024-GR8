using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private float mouseX;

    [SerializeField] private float sensitivity = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * sensitivity;
        //transform.Rotate(0,mouseX,0);
        transform.rotation = Quaternion.Euler(0,mouseX,0);
    }
}
