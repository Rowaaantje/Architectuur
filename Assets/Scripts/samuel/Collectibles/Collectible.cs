using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool PlayerInZone;
    public ObjectType objectType;
    private Collected collected;

    private void Start()
    {
        PlayerInZone = false;
        collected = GetComponentInParent<Collected>();
    }
    private void Update()
    {
        if (PlayerInZone == true) 
        {
            Debug.Log("Item picked up");
        }
    }
    public enum ObjectType
    {
        Planks = 1,
        Nails = 2,
        IronPlates = 3,
        Screw = 4
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = true;
          
        }

        if(objectType == ObjectType.Planks)
        {
            collected.PlankSpite.SetActive(true);
        }
        if(objectType == ObjectType.Nails)
        {
            collected.NailSprite.SetActive(true);
        }
        if (objectType == ObjectType.IronPlates)
        {
            collected.IronplateSprite.SetActive(true);
        }
        if (objectType == ObjectType.Screw)
        {
            collected.ScrewSprite.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
        }
    }

}
