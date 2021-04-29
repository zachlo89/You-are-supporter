using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsLobby : MonoBehaviour
{
    [SerializeField] private Slider sliderEffectsSounds;
    [SerializeField] private Slider musicSlider;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        sliderEffectsSounds.value = audioManager.effectsVolume;
        musicSlider.value = audioManager.backgroundVolume;
    }

    public void ChangeEffectVolume()
    {
        audioManager.EffectVolume(sliderEffectsSounds.value);
    }

    public void ChangeMusicVolume()
    {
        audioManager.BackgroundVolume(musicSlider.value);
    }
}
