using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzel : MonoBehaviour
{
    private int RightOrder = 1234;
    private string rightOrderString;
    private int currentIndex = 0;
    public int LeverIndex = 0;

    private void Start()
    {
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
                Debug.Log("Animation played!");
                currentIndex = 0;
            }
        }   
    }
}


