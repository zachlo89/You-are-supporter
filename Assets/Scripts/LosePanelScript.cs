using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanelScript : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        audioManager.Play("Lose");
    }
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMenu()
    {
        audioManager.StopPlaying();
        audioManager.Play("Lobby");
        SceneManager.LoadScene("Lobby");
    }
}
