using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levers : MonoBehaviour
{
    public string lever;

    public void levers ()
    {
        this.transform.GetComponentInParent<Lever_puzzel>().PasswordEntry(lever);
    }
 
}
