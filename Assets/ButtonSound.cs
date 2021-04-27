using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public void PlayClickSound()
    {
        audioManager.Play("Click");
    }

    public void PlayWinSound()
    {
        audioManager.Play("Win");
    }
}
