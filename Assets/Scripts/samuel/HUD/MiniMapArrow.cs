using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapArrow : MonoBehaviour
{

    public float sensZ;

    public Transform ArrowOrientation;


    float zRotation;

    private PlayerCam playerCam;

    private void Update()
    {
        MouseInput();
        
    }

    void MouseInput()
    {

        float mouseZ = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensZ;


        zRotation += mouseZ; //add the x input to your y rotation 

        ArrowOrientation.rotation = Quaternion.Euler(0, 0, -zRotation);
    }
}
