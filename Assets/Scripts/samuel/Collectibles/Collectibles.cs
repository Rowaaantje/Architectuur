using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private bool PlayerInZone;
    public int CollectibleIndex;
    private int Collected;

    private void Start()
    {
        PlayerInZone = false;
    }
    private void Update()
    {
        if (PlayerInZone == true)
        {
            gameObject.SetActive(false);
            Collected = CollectibleIndex + CollectibleIndex;
        }
        
    }

    private void CollectCollectibles()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = true;
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
