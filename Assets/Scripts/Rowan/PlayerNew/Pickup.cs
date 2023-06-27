using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Pickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;

    [Header("Keybinds")]
    [SerializeField] public KeyCode pickup = KeyCode.E;
    [SerializeField] public KeyCode trow = KeyCode.Mouse0;

    // private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
 
    Rigidbody currentObjectRB;
    
    
    void Update()
    {
        if(currentObjectRB)
        {
            currentObjectRB.MovePosition(Vector3.Lerp(currentObjectRB.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));
 
            if(Input.GetKeyDown(trow))//trow item
            {
                // StopClipping();
                currentObjectRB.isKinematic = false;
                currentObjectRB.useGravity = true;
                currentObjectRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                currentObjectRB = null;
            }
        }
 
            if(Input.GetKeyDown(pickup)) //pickup object
        {
            if(currentObjectRB)
            {
                // StopClipping();
                currentObjectRB.isKinematic = false;
                currentObjectRB.useGravity = true;
                currentObjectRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if(Physics.Raycast(ray, out hit, maxGrabDistance, PickupMask))
                {
                    currentObjectRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if(currentObjectRB)
                    {
                        // currentObjectRB.isKinematic = true;
                        currentObjectRB.useGravity = false;
                    }
                }
            }
        }
    }

//    void RotateObject()
//     {
//         if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
//         {
//             canDrop = false; //make sure throwing can't occur during rotating

//             //disable player being able to look around
//             //mouseLookScript.verticalSensitivity = 0f;
//             //mouseLookScript.lateralSensitivity = 0f;

//             float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
//             float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
//             //rotate the object depending on mouse X-Y Axis
//             heldObj.transform.Rotate(Vector3.down, XaxisRotation);
//             heldObj.transform.Rotate(Vector3.right, YaxisRotation);
//         }
//         else
//         {
//             //re-enable player being able to look around
//             //mouseLookScript.verticalSensitivity = originalvalue;
//             //mouseLookScript.lateralSensitivity = originalvalue;
//             canDrop = true;
//         }
//     }
    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(currentObjectRB.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            currentObjectRB.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }
}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Pickup : MonoBehaviour
// {
//     [SerializeField] private LayerMask PickupMask;
//     [SerializeField] private Camera PlayerCam;
//     [SerializeField] private Transform PickupTarget;
//     [Space]
//     [SerializeField] private float PickupRange;
//     private Rigidbody CurrentObject;
    

//     void Start()
//     {

//     }
//     void Update()
//     {
//         if(Input.GetKeyDown(KeyCode.E))
//         {
//             if(CurrentObject)
//             {
//                 CurrentObject.useGravity = true;
//                 CurrentObject = null;
//                 return;
//             }

//             Ray CameraRay = PlayerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); 
//             if (Physics.Raycast(CameraRay, out RaycastHit HitInfo, PickupRange, PickupMask))
//             {
//                 CurrentObject = HitInfo.rigidbody;
//                 CurrentObject.useGravity = false;
//             }
//         }
//     }

//     void FixedUpdate()
//     {
//         if(CurrentObject)
//         {
//             Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
//             float DistanceToPoint = DirectionToPoint.magnitude;

//             CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint; 
//         }
//     }
// }