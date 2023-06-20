using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject txtToDisplay;
    private bool PlayerInZone;

    private Puzzel puzzle;
    private Animator anim;
    public int OrderIndex;

    private void Start()
    {
        anim = GetComponent<Animator>();
        puzzle = GetComponentInParent<Puzzel>();
        PlayerInZone = false;
        txtToDisplay.SetActive(false);  
    }

    private void Update()
    {
        if (anim != null)
        {
            if (PlayerInZone && Input.GetKeyDown(KeyCode.F))
            {
                anim.SetTrigger("pull_1");
                anim.SetTrigger("pull_2");
                anim.SetTrigger("pull_3");
                anim.SetTrigger("pull_4");
                txtToDisplay.SetActive(false);

                puzzle.LeverIndex = OrderIndex;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }

}
