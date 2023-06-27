using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndQuit : MonoBehaviour
{

    public void LoadScene()
    {
        SceneManager.LoadScene("PlayerTesting");//game scene
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

