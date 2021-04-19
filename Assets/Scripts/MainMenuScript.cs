﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private PersistableSO saveAndLoad;
    [SerializeField] private Button continueButton;
    private void Start()
    {
        if(PlayerPrefs.GetInt("FirstTime", 0) == 1)
        {
            continueButton.interactable = true;
        } else continueButton.interactable = false;
    }
    public void NewGame()
    {
        saveAndLoad.StartAgain();
        PlayerPrefs.SetInt("FirstTime", 1);
        PlayerPrefs.SetInt("Tutorial1", 0);
        PlayerPrefs.SetInt("Tutorial2", 0);
        PlayerPrefs.SetInt("Tutorial3", 0);
        PlayerPrefs.SetInt("Tutorial4", 0);
        PlayerPrefs.SetInt("Tutorial5", -1);
        PlayerPrefs.SetInt("Tutorial6", 0);
        SceneManager.LoadSceneAsync(1);
    }

    public void Continue()
    {
        saveAndLoad.LoadAll();
        PlayerPrefs.SetInt("Tutorial1", -1);
        PlayerPrefs.SetInt("Tutorial2", -1);
        PlayerPrefs.SetInt("Tutorial3", -1);
        PlayerPrefs.SetInt("Tutorial4", -1);
        PlayerPrefs.SetInt("Tutorial5", -1);
        PlayerPrefs.SetInt("Tutorial6", -1);
        SceneManager.LoadSceneAsync(1);
    }
}
