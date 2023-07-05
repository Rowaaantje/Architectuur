using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzel : MonoBehaviour
{
    private int RightOrder = 1234;
    private string rightOrderString;
    private int currentIndex = 0;
    public int LeverIndex = 0;
    private Animator anim;
    private Animator anim2;
    private Animator anim3;
    public GameObject Anim1;
    public GameObject Anim2;
    public GameObject Anim3;

    private void Start()
    {
        anim = Anim1.GetComponent<Animator>();
        anim2 = Anim2.GetComponent<Animator>();
        anim3 = Anim3.GetComponent<Animator>();
        rightOrderString = RightOrder.ToString();
    }

    private void Update()
    {
        string leverIndexString = LeverIndex.ToString();

        if (leverIndexString == rightOrderString[currentIndex].ToString())
        { 
            currentIndex++;

            if (currentIndex == rightOrderString.Length)
            {
                anim.SetTrigger("vijvertje");
                anim2.SetTrigger("waterwiel");
                anim3.SetTrigger("water");
                Debug.Log("Animation played!");
                currentIndex = 0;
            }
        }   
    }
}


