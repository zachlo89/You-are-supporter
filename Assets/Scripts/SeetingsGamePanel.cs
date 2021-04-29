using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SeetingsGamePanel : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private Slider musicSlider, effectSlider;
    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        musicSlider.value = audioManager.backgroundVolume;
        effectSlider.value = audioManager.effectsVolume;
    }
    public void HomeButton()
    {
        Time.timeScale = 1;
        audioManager.StopPlaying();
        audioManager.Play("Lobby");
        SceneManager.LoadScene("Lobby");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");

    }

    public void ChangeEffectVolume()
    {
        audioManager.EffectVolume(effectSlider.value);
    }

    public void ChangeMusicVolume()
    {
        audioManager.BackgroundVolume(musicSlider.value);
    }
}
