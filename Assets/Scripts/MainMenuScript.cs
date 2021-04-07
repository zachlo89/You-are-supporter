using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.SetInt("Tutorial1", 0);
        PlayerPrefs.SetInt("Tutorial2", 0);
        PlayerPrefs.SetInt("Tutorial3", 0);
        PlayerPrefs.SetInt("Tutorial4", 0);
        Debug.Log("Tutorial1 " + (PlayerPrefs.GetInt("Tutorial1", -1)));
        SceneManager.LoadSceneAsync(1);
    }

    public void Continue()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
