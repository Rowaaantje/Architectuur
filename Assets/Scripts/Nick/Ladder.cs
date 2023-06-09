using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class Ladder : MonoBehaviour {
 
     GameObject playerOBJ;
     bool canClimb = false;
     public float speed = 1.5f;
 
     void OnCollisionEnter(Collision coll) // If Player Collides with The Ladder Climbing Will become Possible
     {
         if (coll.gameObject.tag == "Player")
         {
             canClimb = true;
             playerOBJ = coll.gameObject;
         }
     }
 
     void OnCollisionExit(Collision coll2) // If Player exits Collision with The Ladder Climbing Will Not be Possible
    {
         if (coll2.gameObject.tag == "Player")
         {
             canClimb = false;
             playerOBJ = null;
         }
     }
     void Update()
     {
         if (canClimb)
         {
             if (Input.GetKey(KeyCode.W))
             {
                 playerOBJ.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
             }
             if (Input.GetKey(KeyCode.S))
             {
                 playerOBJ.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
             }
         }
     }
 }
