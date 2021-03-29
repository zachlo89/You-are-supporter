using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SeetingsGamePanel : MonoBehaviour
{
    public void HomeButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
