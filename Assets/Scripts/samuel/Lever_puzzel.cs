using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever_puzzel : MonoBehaviour
{

    public string password;
    public int passwordLimit;
    public Text passwordText;
    private void Start()
    {

    }

    public void PasswordEntry(string number)
    {
        int length = passwordText.text.ToString().Length;
        if (length < passwordLimit)
        {
            passwordText.text = passwordText.text + number;
        }
    }




}
