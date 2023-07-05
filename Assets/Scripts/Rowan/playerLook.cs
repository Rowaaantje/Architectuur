 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;


    [SerializeField] Transform cam; //we want to reference the camera holder in the inspector
    

    [SerializeField] Transform orientation;
    float mouseX;
    float mouseY;


    float multiplier = 0.01f;
    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;


        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);  //for the camera we want to set the local rotation to rotate by oure x ratation on the x-axis 
        orientation.transform.rotation = Quaternion.Euler(0, yRotation,  0); //only rotate the player on the y-axis
    }
}
