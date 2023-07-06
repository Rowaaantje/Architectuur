using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ASyncLoader : MonoBehaviour
{
   [Header("Menu Screens")]
   [SerializeField] private GameObject loadingScreen;
   [SerializeField] private GameObject mainMenu;

   [Header("Slider")]
   [SerializeField] private Slider loadingSlider;

    public void LoadLevenButton(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);


        StartCoroutine(loadLevelASync(levelToLoad));
        //Run the A sync
    }

    IEnumerator loadLevelASync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);


        while (!loadOperation.isDone)
        {
            float progresValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progresValue;
            yield return null;
        }
    }
}
